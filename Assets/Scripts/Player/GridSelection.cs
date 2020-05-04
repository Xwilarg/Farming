using UnityEngine;

public class GridSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabSelection;

    private bool _isMe = true;
    private bool _isPlacementEnabled = false;
    private GameObject _selectionGo = null;

    public void SetMe(bool value) => _isMe = value;

    private void Update()
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
        }
    }

    /// <summary>
    /// Update selection position depending of cursor position
    /// </summary>
    private void UpdateSelectionPosition()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        int x = (int)transform.position.x, z = (int)transform.position.z;
        if (mousePos.x < x - .5f) x++;
        else if (mousePos.x > x + .5f) x--;
        if (mousePos.z < z) z++;
        else if (mousePos.z > z + 1) z--;
        _selectionGo.transform.position = new Vector3(x, 0.001f, z);
    }
}
