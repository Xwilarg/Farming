using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject gunImpact;

    public bool DOES_DISAPPEAR_ON_COLLISION { set; private get; }
    public int DAMAGE { set; private get; }
    public Action<Vector3, int> DISPLAY_DAMAGE;

    private void OnCollisionEnter(Collision collision)
    {
        var charac = collision.gameObject.GetComponent<Character>();
        if (charac != null)
        {
            charac.TakeDamage(DAMAGE);
            DISPLAY_DAMAGE(collision.contacts[0].point, DAMAGE);
        }
        if (collision.collider.CompareTag("Floor")) // We display impact on floor
            Destroy(Instantiate(gunImpact, collision.contacts[0].point + (Vector3.up * 0.001f), Quaternion.identity), 3f);
        if (DOES_DISAPPEAR_ON_COLLISION)
            Destroy(gameObject);
    }
}
