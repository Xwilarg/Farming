using UnityEngine;

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
        if (_isMe && !Options.S.IsPaused())
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
                    _selectionGo = Instantiate(_prefabSelection);
                    _selectionRenderer = _selectionGo.GetComponent<MeshRenderer>();
                    _isPlacementEnabled = true;
                    UpdateSelectionPosition();
                    UpdateSelectionColor();
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                var item = UIManager.uiManager.GetActionBar().GetCurrentlySelectedItem();
                if (item == null)
                    return;
                if (item.IsTileCorrect(TileType.Special)) // For things items that can't be placed (like gun who can only be shoot)
                {
                    item.Place(_pc, Vector2Int.zero); // Position doesn't matter in this case
                }
                else if (_isPlacementEnabled)
                {
                    var pos = _selectionGo.transform.position.ToVector2Int();
                    if (Generation.GENERATION.CanSpawnObject(item.GetId(), pos))
                    {
                        // If we are in debug mode (then there is no NetworkManager) or if we are the server
                        if (NetworkManager.NETWORK_MANAGER == null || NetworkManager.NETWORK_MANAGER.RequestItemSpawn(item.GetId(), pos))
                        {
                            GameManager.MANAGER.InstantiateItem(item, pos, _pc.GetPlayer(), true);
                        }
                    }
                }
            }
            if (_isPlacementEnabled)
            {
                UpdateSelectionPosition();
                if (_oldPlacementPos != _selectionGo.transform.position.ToVector2Int()) // TODO: Check need to be done again if a remote player put an object
                    UpdateSelectionColor();
            }
        }
    }

    public void UpdateSelectionColor()
    {
        if (_selectionGo == null)
            return;
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
        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, float.MaxValue, LayerMask.GetMask("Floor"));
        Vector3 pos;
        if (hit.collider != null && Vector3.Distance(transform.position, hit.collider.transform.position) <= _info.placementDist)
            pos = hit.collider.transform.position;
        else
        {
            var tmp = transform.InverseTransformPoint(transform.position + transform.forward);
            tmp.y = 0f;
            pos = transform.TransformPoint(tmp.normalized * _info.placementDist);
        }
        int x = Mathf.RoundToInt(pos.x), z = Mathf.RoundToInt(pos.z);
        if (pos.x < x - .5f) x--;
        else if (pos.x > x + .5f) x++;
        if (pos.z < z) z--;
        else if (pos.z > z + 1) z++;
        _selectionGo.transform.position = new Vector3(x, 0.001f, z);
        _selectionGo.transform.rotation = Quaternion.identity;
    }
}
