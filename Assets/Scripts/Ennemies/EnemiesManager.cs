using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager S;

    [SerializeField]
    private RectTransform _nextWaveProgress;

    private void Awake()
    {
        S = this;
    }

    [SerializeField]
    private EnemySpawnInfo _spawnInfo;

    private List<GameObject> _enemies;

    private float _maxSize;
    private float _timer;

    private void Start()
    {
        _enemies = new List<GameObject>();
        _maxSize = _nextWaveProgress.sizeDelta.x;
        _timer = 0;
    }

    private void Update()
    {
        if (!Generation.GENERATION.HaveGenerator())
            return;
        _nextWaveProgress.parent.gameObject.SetActive(true);
        _timer += Time.deltaTime;
        _nextWaveProgress.sizeDelta = new Vector2(_maxSize - (_timer * _maxSize / _spawnInfo.timeBetweenWaves), _nextWaveProgress.sizeDelta.y);
        if (_timer > _spawnInfo.timeBetweenWaves)
        {
            _timer = 0f;
            InstantiateEnnemies();
        }
    }

    public void InstantiateEnnemies()
    {
        for (int i = 0; i < _spawnInfo.ennemiesPerWaves; i++)
        {
            var randomPos = Random.insideUnitCircle.normalized * _spawnInfo.distanceSpawn;
            var randomPos3 = new Vector3(randomPos.x, 0f, randomPos.y) + new Vector3(PlayerController.LOCAL.transform.position.x, 1f, PlayerController.LOCAL.transform.position.z);
            _enemies.Add(Instantiate(_spawnInfo.prefabEnemy, randomPos3, Quaternion.identity));
        }
    }

    public void DestroyEnemy(GameObject gameObject)
    {
        if (_enemies.Contains(gameObject))
            _enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
