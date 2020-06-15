using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager S;

    private void Awake()
    {
        S = this;
    }

    [SerializeField]
    private EnemySpawnInfo _spawnInfo;

    private float _spawnTimer;

    private List<GameObject> _enemies;

    private void Start()
    {
        _spawnTimer = _spawnInfo.timeBetweenSpawn;
        _enemies = new List<GameObject>();
    }

    private void Update()
    {
        if (!Generation.GENERATION.HaveGenerator())
            return;

        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0f && _enemies.Count < _spawnInfo.maxEnnemiesAtOnce && PlayerController.LOCAL != null)
        {
            var randomPos = Random.insideUnitCircle.normalized * _spawnInfo.distanceSpawn;
            var randomPos3 = new Vector3(randomPos.x, 0f, randomPos.y) + new Vector3(PlayerController.LOCAL.transform.position.x, 1f, PlayerController.LOCAL.transform.position.z);
            InstantiateEnemy(_spawnInfo.prefabEnemy, randomPos3);
            _spawnTimer = _spawnInfo.timeBetweenSpawn;
        }
    }

    public void InstantiateEnemy(GameObject go, Vector3 pos)
    {
        _enemies.Add(Instantiate(go, pos, Quaternion.identity));
    }

    public void DestroyEnemy(GameObject gameObject)
    {
        if (_enemies.Contains(gameObject))
            _enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
