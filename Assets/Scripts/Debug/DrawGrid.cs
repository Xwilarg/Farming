using UnityEngine;

public class DrawGrid : MonoBehaviour
{
    private int _displaySize = 0;
    private GameObject _player;

    public void ToggleGrid()
    {
        if (_displaySize == 0)
        {
            _displaySize = 5;
            _player = GameObject.FindGameObjectWithTag("Player");
        }
        else _displaySize = 0;
    }

    /// <summary>
    /// If the option is enabled we draw a grid where the pkayer is (gizmos must be enabled in Unity Editor)
    /// </summary>
    private void OnDrawGizmos()
    {
        if (_displaySize > 0)
        {
            Gizmos.color = Color.white;
            int xPos = Mathf.RoundToInt(_player.transform.position.x), zPos = Mathf.RoundToInt(_player.transform.position.z);
            for (int i = -_displaySize; i < _displaySize; i++)
            {
                Gizmos.DrawLine(new Vector3(xPos + _displaySize - .5f, 0f, zPos + i + .5f), new Vector3(xPos - _displaySize + .5f, 0f, zPos + i + .5f));
                Gizmos.DrawLine(new Vector3(xPos + i + .5f, 0f, zPos + _displaySize - .5f), new Vector3(xPos + i + .5f, 0f, zPos - _displaySize + .5f));
            }
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(new Vector3(xPos, 0f, zPos), .5f);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * _displaySize);
        }
    }
}
