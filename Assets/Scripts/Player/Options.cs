using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    private Transform _optionPanel;
    private Console _console;

    public static Options S;

    [SerializeField]
    private GameObject _gameUI;

    [SerializeField]
    private OptionsInfo _info;
    public OptionsInfo GetInfo() => _info;

    private void Awake()
    {
        S = this;
    }

    [DllImport("user32.dll")] public static extern IntPtr GetKeyboardLayout(int idThread);

    private void Start()
    {
        _optionPanel = transform.GetChild(0);
        _console = null;

        // Make sure that option panel is closed on game start
        _optionPanel.gameObject.SetActive(false);

        // Detect if user have AZERTY keyboard
        var hkl = GetKeyboardLayout(0);
        int id = (int)((uint)hkl.ToInt32() & 0x0000FFFF);
        bool haveAzertyKeyboard = id == 2060 || id == 1036; // https://docs.microsoft.com/en-us/windows-hardware/manufacture/desktop/windows-language-pack-default-values
        if (haveAzertyKeyboard)
        {
            foreach (var info in typeof(OptionsInfo).GetFields())
            {
                if (info.FieldType == typeof(KeyCode))
                {
                    var value = (KeyCode)info.GetValue(_info);
                    if (value == KeyCode.A) info.SetValue(_info, KeyCode.Q);
                    else if (value == KeyCode.Q) info.SetValue(_info, KeyCode.A);
                    else if (value == KeyCode.W) info.SetValue(_info, KeyCode.Z);
                    else if (value == KeyCode.Z) info.SetValue(_info, KeyCode.W);
                }
            }
        }
    }

    public bool IsPaused()
        => _optionPanel.gameObject.activeInHierarchy;

    public void RespawnConsole()
    {
        if (_console == null)
            _console = GameObject.FindGameObjectWithTag("DebugManager").GetComponent<Console>();
        _console.Open();
    }

    /// <summary>
    /// Open the folder where game saves are
    /// </summary>
    public void OpenSaveFolder()
    {
        System.Diagnostics.Process.Start(Application.persistentDataPath); // Not sure if this is cross-plateform
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!UIManager.uiManager.CloseUI()) // If there was others windows to close we don't open the options menu
            {
                if (_optionPanel.gameObject.activeInHierarchy)
                {
                    _optionPanel.gameObject.SetActive(false);
                    _console?.Close();
                    _gameUI.SetActive(true);
                    if (SceneManager.GetActiveScene().name == "Main") // If we are on the Main scene we lock the mouse because of FPS view
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                }
                else
                {
                    _optionPanel.gameObject.SetActive(true);
                    _gameUI.SetActive(false); // Hide game UI (crosshair, action bar, etc) on option menu
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        };
    }
}
