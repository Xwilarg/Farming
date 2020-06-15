using System;
using UnityEngine;
using UnityEngine.UI;

public class KeyBinding : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Field name from OptionsInfo.cs")]
    private string _keyString;

    [SerializeField]
    private Text _currentKey;

    private bool _waitingForBinding;

    private void Start()
    {
        _waitingForBinding = false;
        _currentKey.text = Options.S.GetStringKey(_keyString);
    }

    private void Update()
    {
        if (_waitingForBinding && Input.anyKeyDown)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    Options.S.BindKey(_keyString, key);
                    _currentKey.text = key.ToString();
                    _waitingForBinding = false;
                    break;
                }
            }
        }
    }

    public void WaitForBinding()
    {
        _currentKey.text = "...";
        _waitingForBinding = true;
    }
}
