using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ennemy : MonoBehaviour
{
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform);
        _rb.velocity = transform.forward * 1f;
    }
}
