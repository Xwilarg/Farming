using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager S;

    private void Awake()
    {
        S = this;
    }

    [SerializeField]
    private TimerInfo info;

    private Dictionary<Vector2Int, float> wetDirt;
    public void AddWetDirt(Vector2Int pos)
    {
        if (wetDirt.ContainsKey(pos))
            wetDirt[pos] = info.dirtWetTimer;
        else
            wetDirt.Add(pos, info.dirtWetTimer);
    }

    private void Start()
    {
        wetDirt = new Dictionary<Vector2Int, float>();
    }

    private void Update()
    {
        var keys = wetDirt.Select(x => x.Key).ToArray();
        for (int i = keys.Length - 1; i >= 0; i--)
        {
            var pos = keys[i];
            var newTime = wetDirt[pos] - Time.deltaTime;
            if (newTime < 0)
            {
                Generation.GENERATION.ChangeFloorType(pos, TileType.Dirt);
                wetDirt.Remove(pos);
            }
            else
            {
                wetDirt[pos] = newTime;
            }
        }
    }
}
