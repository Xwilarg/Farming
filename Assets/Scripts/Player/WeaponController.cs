using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private GameObject _laser, _bullet;

    private const float _baseFov = 60f; // The default FoV of the main camera
    private bool isZoomed;

    private void Start()
    {
        isZoomed = false;
    }

    public void Shoot(WeaponInfo info)
    {
        for (int i = 0; i < info.nbShoot; i++)
        {
            if (info.shootType == WeaponShootType.Laser)
            {
                Vector3 destination;
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, float.MaxValue, ~LayerMask.GetMask("Player")))
                    destination = hit.point;
                else
                    destination = Camera.main.transform.position + (Camera.main.transform.forward * 100f);
                var go = Instantiate(_laser); // TODO: Calculate deviation
                var lr = go.GetComponent<LineRenderer>();
                lr.SetPositions(new[] { transform.position, destination });
                Destroy(go, info.lifetime);
            }
            else if (info.shootType == WeaponShootType.Bullet)
            {
                float d = info.deviationAngle / 2f;
                var go = Instantiate(_bullet, Camera.main.transform.position, Quaternion.identity);
                var rb = go.GetComponent<Rigidbody>();
                rb.velocity = Camera.main.transform.forward.normalized * info.projectileSpeed + new Vector3(Random.Range(-d, d), Random.Range(-d, d), Random.Range(-d, d));
                rb.mass = info.projectileMass;
                Destroy(go, info.lifetime);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var item = UIManager.uiManager.GetActionBar().GetCurrentlySelectedItem();
            if (item.IsTileCorrect(TileType.Special))
            {
                if (!isZoomed)
                    Camera.main.fieldOfView = _baseFov / 1.5f;
                else
                    Camera.main.fieldOfView = _baseFov;
                isZoomed = !isZoomed;
            }
        }
    }

    public void ResetZoom()
    {
        Camera.main.fieldOfView = _baseFov;
        isZoomed = false;
    }
}
