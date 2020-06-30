using System;
using System.IO;
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


    private string _controlsSavePath;

    [DllImport("user32.dll")] public static extern IntPtr GetKeyboardLayout(int idThread);

    private void Awake()
    {
        S = this;

        _controlsSavePath = Application.persistentDataPath + "/controls.txt";

        // We load controls in Awake cause some others scripts will use them in their Start
        OptionsInfo newInfo = ScriptableObject.CreateInstance<OptionsInfo>();
        foreach (var info in typeof(OptionsInfo).GetFields())
        {
            info.SetValue(newInfo, info.GetValue(_info));
        }
        _info = newInfo;

        if (File.Exists(_controlsSavePath))
        {
            try
            {
                var ms = new MemoryStream(File.ReadAllBytes(_controlsSavePath));
                var reader = new BinaryReader(ms);
                foreach (var info in typeof(OptionsInfo).GetFields())
                {
                    if (info.FieldType == typeof(float))
                        info.SetValue(_info, reader.ReadSingle());
                    else if (info.FieldType == typeof(bool))
                        info.SetValue(_info, reader.ReadBoolean());
                    else if (info.FieldType == typeof(KeyCode))
                        info.SetValue(_info, reader.ReadKeyCode());
                    else
                        throw new ArgumentException("Invalid field type " + info.FieldType.Name + " while parsing OptionsInfo");
                }
            } catch (EndOfStreamException) // The configuration file content is probably out of date
            {
                File.Delete(_controlsSavePath);
                Awake();
                return;
            }
        }
        else
        {
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
            SaveControls();
        }
    }

    private void Start()
    {
        _optionPanel = transform.GetChild(0);
        _console = null;

        // Make sure that option panel is closed on game start
        _optionPanel.gameObject.SetActive(false);
    }

    private void SaveControls()
    {
        var ms = new MemoryStream();
        var writer = new BinaryWriter(ms);
        foreach (var info in typeof(OptionsInfo).GetFields())
        {
            if (info.FieldType == typeof(float)) writer.Write((float)info.GetValue(_info));
            if (info.FieldType == typeof(bool)) writer.Write((bool)info.GetValue(_info));
            if (info.FieldType == typeof(KeyCode)) writer.Write((KeyCode)info.GetValue(_info));
        }
        File.WriteAllBytes(Application.persistentDataPath + "/controls.txt", ms.ToArray());
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

    public void DeleteSaves()
    {
        if (File.Exists(_controlsSavePath)) File.Delete(_controlsSavePath);
        Popup.S.Show("Information", "Your saves were deleted.");
    }

    /// <summary>
    /// Rebind a key
    /// </summary>
    /// <param name="keyString">Field name in OptionsInfo.cs</param>
    /// <param name="key">New key</param>
    public void BindKey(string keyString, KeyCode key)
    {
        typeof(OptionsInfo).GetField(keyString).SetValue(_info, key);
        SaveControls();
    }

    public string GetStringKey(string keyString)
    {
        return typeof(OptionsInfo).GetField(keyString).GetValue(_info).ToString();
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
