﻿using System.Collections.Generic;
using UnityEngine;

using Chunk = System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, TileInfo>;

public class Generation : MonoBehaviour
{
    public static Generation GENERATION;

    [SerializeField]
    private GameObject _prefabFloor;

    [SerializeField]
    private TileMaterials _materials;

    [SerializeField]
    private GenerationInfo _gen;

    private Dictionary<Vector2Int, Chunk> _instantiated;
    private Dictionary<TileType, Material> _tileTypes;

    private Transform _floorContainer;

    private void Awake()
    {
        GENERATION = this;
    }

    private void Start()
    {
        _tileTypes = new Dictionary<TileType, Material>
        {
            { TileType.Sand, _materials.Sand },
            { TileType.Grass, _materials.Grass }
        };
        _instantiated = new Dictionary<Vector2Int, Chunk>();

        _floorContainer = new GameObject("Floor Container").transform;

        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
                InstantiateChunk(new Vector2Int(x * _gen.ChunkSize, y * _gen.ChunkSize));
    }

    private void InstantiateChunk(Vector2Int pos)
    {
        if (!_instantiated.ContainsKey(pos / _gen.ChunkSize))
        {
            var chunk = new Chunk();
            Transform chunkParent = new GameObject("Chunk " + (pos.x / _gen.ChunkSize) + "_" + (pos.y / _gen.ChunkSize)).transform;
            for (int x = 0; x < _gen.ChunkSize; x++)
                for (int y = 0; y < _gen.ChunkSize; y++)
                {
                    chunkParent.parent = _floorContainer;
                    var go = Instantiate(_prefabFloor, chunkParent);
                    go.transform.position = new Vector3(pos.x + x - (_gen.ChunkSize / 2), 0f, pos.y + y - (_gen.ChunkSize / 2));
                    go.name = "Floor " + x + "_" + y;
                    go.GetComponent<MeshRenderer>().material = _tileTypes[TileType.Sand];
                    chunk.Add(new Vector2Int(x, y), new TileInfo(go));
                }
            _instantiated.Add(pos / _gen.ChunkSize, chunk);
        }
    }

    public void ChangeFloorType(Vector2Int pos, TileType newType)
    {
        // TODO: Remove any object that can't be on the new floor type
        var tile = GetTile(pos);
        tile.SetTileType(newType, _tileTypes[newType]);
    }

    public bool CanSpawnObject(ItemID id, Vector2Int pos)
    {
        var tile = GetTile(pos);
        return tile.CanAddItem(ItemsList.Items.AllItems[id]);
    }

    public void SpawnObject(ItemID id, Vector2Int pos)
    {
        var tile = GetTile(pos);
        var item = ItemsList.Items.AllItems[id];
        tile.AddItem(item, Instantiate(item.GetGameObject(), new Vector3(pos.x, .5f, pos.y), Quaternion.identity));
    }

    private TileInfo GetTile(Vector2Int pos)
    {
        var chunkPos = ((Vector2)pos / (_gen.ChunkSize / 2f + 0.001f)).ToVector2Int();
        var tilePos = (chunkPos.Multiply(_gen.ChunkSize) - pos.Add(_gen.ChunkSize / 2)).Abs();
        InstantiateChunk(chunkPos); // We make sure that the chunk that contains the object is instantiated
        Debug.Log("Requesting position " + pos + ", getting chunk " + chunkPos + " with position " + tilePos);
        return _instantiated[chunkPos][tilePos];
    }
}
