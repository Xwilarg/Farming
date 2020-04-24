using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private Material _green, _blue;

    public void InstantiatePlayer(Player p, NetworkManager net, bool isMe, Vector2 position, Vector2Int velocity)
    {
        GameObject go = Instantiate(_playerPrefab, new Vector3(position.x, 1f, position.y), Quaternion.identity);
        PlayerController pc = go.GetComponent<PlayerController>();
        pc.InitNetwork(p, net, isMe);
        pc.UpdatePosition(position, velocity);
        p.Pc = pc;
        go.GetComponent<Renderer>().material = isMe ? _green : _blue;
    }
}
