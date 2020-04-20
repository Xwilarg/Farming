using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private const float _speed = 10f;
    private bool _isMe = true;

    public void SetIsMe(bool value) => _isMe = value;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_isMe) // Is local player
        {
            _rb.velocity = new Vector3(Input.GetAxis("Horizontal") * _speed, _rb.velocity.y, Input.GetAxis("Vertical") * _speed);
        }
    }
}
