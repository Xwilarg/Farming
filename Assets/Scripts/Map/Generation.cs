using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabFloor;

    private Dictionary<Vector2Int, GameObject> _instantiated;

    private void Start()
    {
        _instantiated = new Dictionary<Vector2Int, GameObject>();
        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
                InstantiateChunk(new Vector2Int(x, y));
    }

    private void InstantiateChunk(Vector2Int pos)
    {
        if (!_instantiated.ContainsKey(pos))
            _instantiated.Add(pos, Instantiate(_prefabFloor, new Vector3(pos.x * 10f, 0f, pos.y * 10f), Quaternion.identity));
    }
}
