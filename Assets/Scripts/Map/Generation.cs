using System.Collections.Generic;
using UnityEngine;

using Chunk = System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, TileInfo>;

public class Generation : MonoBehaviour
{
    public static Generation GENERATION;

    [SerializeField]
    private GameObject _prefabFloor;

    [SerializeField]
    private Material _sand, _grass;

    private Dictionary<Vector2Int, Chunk> _instantiated;

    private void Awake()
    {
        GENERATION = this;
    }

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

    public bool SpawnObject(ItemID id, Vector2Int pos)
    {
        var chunkPos = pos / 10;
        InstantiateChunk(pos); // We make sure that the chunk that contains the object is instantiated
        var tile = _instantiated[pos][pos.Modulo(10)];
        var item = ItemsList.Items.AllItems[id];
        if (!tile.CanAddItem(item))
            return false;
        tile.AddItem(item, Instantiate(item.GetGameObject(), new Vector3(pos.x, .5f, pos.y), Quaternion.identity));
        return true;
    }
}
