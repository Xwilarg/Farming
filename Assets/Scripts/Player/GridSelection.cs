using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class GridSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabSelection;

    [SerializeField]
    private Material _validPositionMaterial, _invalidPositionMaterial;

    private bool _isMe = true;
    private bool _isPlacementEnabled = false;
    private GameObject _selectionGo = null;
    private MeshRenderer _selectionRenderer;

    private PlayerController _pc;
    private Vector2Int? _oldPlacementPos;

    private PlayerInfo _info;

    public void Init(PlayerInfo info)
    {
        _info = info;
    }
    public void SetMe(bool value) => _isMe = value;

    private void Start()
    {
        _pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_isMe && !Console.S.IsConsoleOpened)
        {
            if (Input.GetKeyDown(_info.placementKey)) // Press Q (by default) to enable/disable selection mode
            {
                if (_isPlacementEnabled)
                {
                    Destroy(_selectionGo);
                    _selectionGo = null;
                    _isPlacementEnabled = false;
                }
                else
                {
                    _selectionGo = Instantiate(_prefabSelection, transform);
                    _selectionRenderer = _selectionGo.GetComponent<MeshRenderer>();
                    _isPlacementEnabled = true;
                    UpdateSelectionPosition();
                    UpdateSelectionColor();
                }
            }
            if (_isPlacementEnabled)
            {
                UpdateSelectionPosition();
                if (Input.GetMouseButtonDown(0))
                {
                    var item = UIManager.uiManager.GetActionBar().GetCurrentlySelectedItem();
                    var pos = _selectionGo.transform.position.ToVector2Int();
                    if (item != null && Generation.GENERATION.CanSpawnObject(item.GetId(), pos))
                    {
                        // If we are in debug mode (then there is no NetworkManager) or if we are the server
                        if (NetworkManager.NETWORK_MANAGER == null || NetworkManager.NETWORK_MANAGER.RequestItemSpawn(item.GetId(), pos))
                        {
                            GameManager.MANAGER.InstantiateItem(item, pos, _pc.GetPlayer(), true);
                        }
                    }
                }
                if (_oldPlacementPos == null || _oldPlacementPos != _selectionGo.transform.position.ToVector2Int()) // TODO: Check need to be done again if a remote player put an object
                    UpdateSelectionColor();
            }
        }
    }

    public void UpdateSelectionColor()
    {
        var pos = _selectionGo.transform.position.ToVector2Int();
        var item = UIManager.uiManager.GetActionBar().GetCurrentlySelectedItem();
        if (item == null || Generation.GENERATION.CanSpawnObject(item.GetId(), pos))
            _selectionRenderer.material = _validPositionMaterial;
        else
            _selectionRenderer.material = _invalidPositionMaterial;
        _oldPlacementPos = pos;
    }

    /// <summary>
    /// Update selection position depending of cursor position
    /// </summary>
    private void UpdateSelectionPosition()
    {
        // We don't want to display the selection tile when holding a special item (ex: gun)
        if (UIManager.uiManager.GetActionBar().GetCurrentlySelectedItem().IsTileCorrect(TileType.Special)) // TODO: There is probably a better way to do that than every Update loop
        {
            _selectionGo.SetActive(false);
            return;
        }
        _selectionGo.SetActive(true);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward, out hit, float.MaxValue, LayerMask.GetMask("Floor"))
            && Vector3.Distance(transform.position, hit.transform.position) <= _info.placementDist)
        {
            var pos = hit.point;
            int x = Mathf.RoundToInt(transform.position.x), z = Mathf.RoundToInt(transform.position.z);
            if (pos.x < x - .5f) x--;
            else if (pos.x > x + .5f) x++;
            if (pos.z < z) z--;
            else if (pos.z > z + 1) z++;
            _selectionGo.transform.position = new Vector3(x, 0.001f, z);
        }
    }
}
