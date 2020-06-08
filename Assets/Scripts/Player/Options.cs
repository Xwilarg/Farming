﻿using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    private Transform _optionPanel;
    private Console _console;

    public static Options S;

    [SerializeField]
    private GameObject _gameUI;

    private void Awake()
    {
        S = this;
    }

    private void Start()
    {
        _optionPanel = transform.GetChild(0);
        _console = null;

        // Make sure that option panel is closed on game start
        _optionPanel.gameObject.SetActive(false);
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
        Process.Start(Application.persistentDataPath); // Not sure if this is cross-plateform
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
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
        };
    }
}
