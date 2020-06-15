using UnityEngine;

public class EnableChildrenOnGameStart : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(true);
    }
}
