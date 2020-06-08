using UnityEngine;

public class Character : MonoBehaviour
{
    private int hp;

    private void Start()
    {
        hp = 10;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
            Destroy(gameObject);
    }
}
