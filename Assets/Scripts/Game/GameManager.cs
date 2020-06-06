using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private GameObject _debugManager;

    [SerializeField]
    private Material _green, _blue;

    private Vector3 _cameraBaseOffset;
    private Camera _worldCamera;

    private Console _console;

    public static GameManager MANAGER;

    private void Awake()
    {
        MANAGER = this;
    }

    private void Start()
    {
        _worldCamera = GameObject.FindGameObjectWithTag("WorldCamera").GetComponent<Camera>();
        _cameraBaseOffset = _worldCamera.transform.position;
        if (GameObject.FindGameObjectWithTag("DebugManager") == null) // For debug
            Instantiate(_debugManager);
        // We create a new player without TCP link in case we started the game skipping the connection phase
        GameObject.FindGameObjectWithTag("DebugManager").GetComponent<Console>().EnablePlayerSpawn(new Player(null, 255, true));
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void InstantiatePlayer(Player p, NetworkManager net, bool isMe, Vector2 position, Vector2 velocity)
    {
        GameObject go = Instantiate(_playerPrefab);
        PlayerController pc = go.GetComponent<PlayerController>();
        pc.InitNetwork(p, net, isMe);
        pc.UpdatePosition(position, velocity);
        p.Pc = pc;
        go.GetComponent<Renderer>().material = isMe ? _green : _blue;
        if (isMe) // Is this is the current player, we set the camera as a child so it follow the player
        {
            _worldCamera.transform.position = _cameraBaseOffset;
            _worldCamera.transform.parent = go.transform;
            go.tag = "Player"; // We set the "Player" tag only on the local player
            GameObject.FindGameObjectWithTag("DebugManager").GetComponent<Console>().EnablePlayerSpawn(p);
            p.Inventory.InitInventoryContent();
        }
        else
            go.GetComponentInChildren<Camera>().gameObject.SetActive(false); // Disable FPS camera
    }

    public void InstantiateItem(Item item, Vector2Int pos, Player p, bool doesUpdateInventory)
    {
        if (item.HaveGameObject())
        {
            Generation.GENERATION.SpawnObject(item.GetId(), pos);
            if (doesUpdateInventory)
                p.Inventory.RemoveItem(item.GetId());
            item.Place(p.Pc, pos);
        }
    }
}
