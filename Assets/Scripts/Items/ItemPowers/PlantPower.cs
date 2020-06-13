
using UnityEngine;

public class PlantPower : AItemPower
{
    public PlantPower(PlantInfo info)
    {
        _info = info;
    }

    private PlantInfo _info;
}