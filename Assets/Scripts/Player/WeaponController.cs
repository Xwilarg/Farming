using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private GameObject _laser, _bullet;

    public void Shoot(WeaponInfo info)
    {
        if (info.shootType == WeaponShootType.Laser)
        {
            Vector3 destination;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, float.MaxValue, ~LayerMask.GetMask("Player")))
                destination = hit.point;
            else
                destination = Camera.main.transform.position + (Camera.main.transform.forward * 100f);
            var go = Instantiate(_laser);
            var lr = go.GetComponent<LineRenderer>();
            lr.SetPositions(new[] { transform.position, destination });
            Destroy(go, info.lifetime);
        }
        else if (info.shootType == WeaponShootType.Bullet)
        {
            var go = Instantiate(_bullet, Camera.main.transform.position, Quaternion.identity);
            var rb = go.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * info.projectileSpeed;
            rb.mass = info.projectileMass;
            Destroy(go, info.lifetime);
        }
    }
}
