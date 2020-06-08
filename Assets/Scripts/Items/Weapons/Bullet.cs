using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject gunImpact;

    private Rigidbody rb;

    public bool DOES_DISAPPEAR_ON_COLLISION { set; private get; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor")) // We display impact on floor
            Destroy(Instantiate(gunImpact, collision.contacts[0].point + (Vector3.up * 0.001f), Quaternion.identity), 3f);
        if (DOES_DISAPPEAR_ON_COLLISION)
            Destroy(gameObject);
    }
}
