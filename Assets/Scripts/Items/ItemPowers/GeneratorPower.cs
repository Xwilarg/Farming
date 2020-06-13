using UnityEngine;

public class GeneratorPower : AItemPower
{
    public override void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    {
        int power = ItemsInit.S.mainGeneratorInfo.Power;
        for (int x = -power; x <= power; x++)
        {
            for (int y = -power; y <= power; y++)
            {
                if (Mathf.Abs(x) + Mathf.Abs(y) < power)
                {
                    Generation.GENERATION.ChangeFloorType(pos + new Vector2Int(x, y), TileType.Grass);
                }
            }
        }
    }
}
