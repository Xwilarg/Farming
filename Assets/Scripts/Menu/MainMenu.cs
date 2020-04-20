using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private InputField _ipField, _portField;

    [SerializeField]
    private Button _hostButton, _joinButton;

    [SerializeField]
    private GameObject _connectingPopup;

    private NetworkManager _network;

    private void Start()
    {
        _network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Update()
    {
        // We don't want the user to click on a button before he filled the input fields
        if (!string.IsNullOrWhiteSpace(_portField.text))
        {
            _hostButton.interactable = true;
            if (!string.IsNullOrWhiteSpace(_ipField.text))
                _joinButton.interactable = true;
            else
                _joinButton.interactable = false;
        }
        else
        {
            _hostButton.interactable = false;
            _joinButton.interactable = false;
        }
    }

    public void Host()
    {
        _network.Host(int.Parse(_portField.text));
        SceneManager.LoadScene("Main");
    }

    public void Connect()
    {
        _connectingPopup.SetActive(true);
        _network.Connect(_ipField.text, int.Parse(_portField.text));
        SceneManager.LoadScene("Main");
    }
}
