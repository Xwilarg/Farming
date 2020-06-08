using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private GameObject _laser, _bullet;

    [SerializeField]
    private GameObject _damagePrefab;

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
                {
                    destination = hit.point;
                    var charac = hit.collider.GetComponent<Character>();
                    if (charac != null)
                    {
                        charac.TakeDamage(info.damage);
                        DisplayDamage(hit.point, info.damage);
                    }
                }
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
                var bullet = go.GetComponent<Bullet>();
                bullet.DOES_DISAPPEAR_ON_COLLISION = info.doesDisappearOnCollision;
                bullet.DAMAGE = info.damage;
                bullet.DISPLAY_DAMAGE = DisplayDamage;
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
            if (item != null)
            {
                if (!isZoomed)
                    Camera.main.fieldOfView = _baseFov / item.GetZoomMultiplicator();
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

    public void DisplayDamage(Vector3 impact, int damage)
    {
        var go = Instantiate(_damagePrefab.gameObject, impact + Random.onUnitSphere, Quaternion.identity);
        var tm = go.GetComponent<TextMesh>();
        tm.characterSize = damage / 10f;
        tm.text = "-" + damage;
    }
}
