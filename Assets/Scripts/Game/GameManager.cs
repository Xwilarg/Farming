using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private GameObject _debugManager;

    [SerializeField]
    private Material _green, _blue;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("DebugManager") == null) // For debug
            Instantiate(_debugManager);
    }

    public void InstantiatePlayer(Player p, NetworkManager net, bool isMe, Vector2 position, Vector2Int velocity)
    {
        GameObject go = Instantiate(_playerPrefab);
        PlayerController pc = go.GetComponent<PlayerController>();
        pc.InitNetwork(p, net, isMe);
        pc.UpdatePosition(position, velocity);
        p.Pc = pc;
        go.GetComponent<Renderer>().material = isMe ? _green : _blue;
    }
}
