using System.IO;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerInfo _info;

    private Rigidbody _rb;
    private bool _isMe = true;
    private Vector2 _oldAxis2D;
    private Vector2 _axis2D; // Controls pressed on X/Z axis
    private Vector3 _pos;
    private Vector2? _newPos = null;

    private Player _player;
    private NetworkManager _net;
    private GridSelection _grid;

    public static PlayerController LOCAL; // Static reference to local player

    public Player GetPlayer()
        => _player;

    public void InitNetwork(Player player, NetworkManager net, bool isMe)
    {
        _player = player;
        _net = net;
        _isMe = isMe;
        if (_isMe)
            LOCAL = this;
        _grid.SetMe(_isMe);
    }

    private void Awake()
    {
        _grid = GetComponent<GridSelection>(); // Called in Awake so it can be ready for InitNetwork
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>().intensity = _info.sunIntensity;
    }

    private void FixedUpdate()
    {
        if (_newPos != null)
        {
            transform.position = new Vector3(_newPos.Value.x, transform.position.y, _newPos.Value.y);
            _newPos = null;
        }
        if (_isMe && !Console.S.IsConsoleOpened) // Is local player and console is closed
        {
            _pos = transform.position;
            int x = 0, y = 0;
            if (Input.GetKey(_info.forwardKey)) y = 1;
            else if (Input.GetKey(_info.backwardKey)) y = -1;
            if (Input.GetKey(_info.leftKey)) x = -1;
            else if (Input.GetKey(_info.rightKey)) x = 1;
            _axis2D = (new Vector2(transform.forward.x, transform.forward.z) * y) + (new Vector2(transform.right.x, transform.right.z) * x);
            _axis2D.Normalize();
            if (_axis2D != _oldAxis2D && _net != null) // If the player changed his movement we update it on the server
            {
                _net.SendRequest(NetworkRequest.PlayerPosition, GetPositionData());
                _oldAxis2D = _axis2D;
            }
            float rotX = Input.GetAxis("Mouse X");
            float rotY = Input.GetAxis("Mouse Y");
            transform.Rotate(rotX * Vector3.up * _info.xSensibivity * (_info.invertXAxis ? -1 : 1));
            Camera.main.transform.Rotate(rotY * Vector3.left * _info.ySensibility * (_info.invertYAxis ? -1 : 1));
        }
        _rb.velocity = new Vector3(_axis2D.x * _info.speed, _rb.velocity.y, _axis2D.y * _info.speed);
    }

    public void UpdateSelectionColor()
    {
        _grid.UpdateSelectionColor();
    }

    public byte[] GetPositionData()
    {
        MemoryStream ms = new MemoryStream();
        BinaryWriter w = new BinaryWriter(ms);
        w.Write(_player.Id);
        w.Write(_pos.x);
        w.Write(_pos.z);
        w.Write(_axis2D.x);
        w.Write(_axis2D.y);
        return ms.ToArray();
    }

    public void UpdatePosition(Vector2 pos, Vector2 vel)
    {
        _newPos = pos;
        _axis2D = vel;
        _oldAxis2D = vel;
    }
}
