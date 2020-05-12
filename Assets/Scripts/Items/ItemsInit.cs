using UnityEngine;

public class ItemsInit : MonoBehaviour
{
    [SerializeField]
    private ItemsList items;

    private void Awake()
    {
        items.Init();
    }
}
