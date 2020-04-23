using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private Material _green, _blue;

    public PlayerController InstantiatePlayer(NetworkManager net, bool isMe, Vector3 position, Vector3 velocity)
    {
        GameObject go = Instantiate(_playerPrefab, position, Quaternion.identity);
        PlayerController pc = go.GetComponent<PlayerController>();
        pc.InitNetwork(net, isMe);
        go.GetComponent<Renderer>().material = isMe ? _green : _blue;
        return pc;
    }
}
