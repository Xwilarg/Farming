using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class GridSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabSelection;

    private bool _isMe = true;
    private bool _isPlacementEnabled = false;
    private GameObject _selectionGo = null;

    private PlayerController pc;

    public void SetMe(bool value) => _isMe = value;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_isMe)
        {
            if (Input.GetKeyDown(KeyCode.Q)) // Press Q to enable/disable selection mode
            {
                if (_isPlacementEnabled)
                {
                    Destroy(_selectionGo);
                    _isPlacementEnabled = false;
                }
                else
                {
                    _selectionGo = Instantiate(_prefabSelection, transform);
                    _isPlacementEnabled = true;
                    UpdateSelectionPosition();
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
                        if (NetworkManager.NETWORK_MANAGER == null || NetworkManager.NETWORK_MANAGER.InstantiateObject(item.GetId(), pos))
                        {
                            Generation.GENERATION.SpawnObject(item.GetId(), pos);
                            item.Place(pos);
                            pc.GetPlayer().Inventory.RemoveItem(item.GetId());
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Update selection position depending of cursor position
    /// </summary>
    private void UpdateSelectionPosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("Floor")))
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
