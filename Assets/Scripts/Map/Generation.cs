using System.Collections.Generic;
using UnityEngine;

using Chunk = System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, TileInfo>;

public class Generation : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabFloor;

    [SerializeField]
    private Material _sand, _grass;

    private Dictionary<Vector2Int, Chunk> _instantiated;


    private void Start()
    {
        _instantiated = new Dictionary<Vector2Int, Chunk>();
        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
                InstantiateChunk(new Vector2Int(x, y));
    }

    private void InstantiateChunk(Vector2Int pos)
    {
        if (!_instantiated.ContainsKey(pos))
        {
            var chunk = new Chunk();
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                {
                    var go = Instantiate(_prefabFloor, new Vector3(pos.x * 10f, 0f, pos.y * 10f), Quaternion.identity);
                    go.GetComponent<MeshRenderer>().material = _sand;
                    chunk.Add(new Vector2Int(x, y), new TileInfo(go));
                }
            _instantiated.Add(pos, chunk);
        }
    }
}
