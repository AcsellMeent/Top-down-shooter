using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Zenject;

public class EnemySpawner : MonoBehaviour, IPauseHandler
{
    public event Action<int> OnEnemyDeath;

    [SerializeField]
    private EnemyChanceConfig[] _enemyChanceConfigs;

    [SerializeField]
    private EnemyMovement[] _enemyPrefabs;

    private PlayerHealth _playerHealth;

    private Camera _mainCamera;

    private Bounds _mapBounds;

    [SerializeField]
    private Vector3 _boundsOffset;

    [SerializeField]
    private float _spawnInterval;

    private PauseManager _pauseManager;

    [Inject]
    public void Construct(PauseManager pauseManager, PlayerHealth playerHealth, MapLimiter mapLimiter)
    {
        _pauseManager = pauseManager;
        _pauseManager.Register(this);
        _playerHealth = playerHealth;
        _mapBounds = mapLimiter.GetBounds();
        _mapBounds = new Bounds(_mapBounds.center, _mapBounds.size - _boundsOffset);
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        if (_mainCamera == null || _enemyPrefabs.Length == 0) throw new Exception("Enemy spawner parameter exception");
        StartCoroutine(SpawnEnemyWithInterval());
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 minCameraPosition = _mainCamera.ViewportToWorldPoint(new Vector2(0, 0)) - _boundsOffset;
        Vector2 maxCameraPosition = _mainCamera.ViewportToWorldPoint(new Vector2(1, 1)) + _boundsOffset;

        Vector2 result = new Vector2();

        List<int> spawnPositions = new List<int>();

        if (minCameraPosition.x - _mapBounds.min.x > 0) spawnPositions.Add(0);
        if (_mapBounds.max.y - maxCameraPosition.y > 0) spawnPositions.Add(1);
        if (_mapBounds.max.x - maxCameraPosition.x > 0) spawnPositions.Add(2);
        if (minCameraPosition.y - _mapBounds.min.y > 0) spawnPositions.Add(3);

        if (spawnPositions.Count == 0) throw new Exception("No sides");

        switch (spawnPositions[Random.Range(0, spawnPositions.Count)])
        {
            case 0:
                result = new Vector2(Random.Range(_mapBounds.min.x, minCameraPosition.x), Random.Range(_mapBounds.min.y, _mapBounds.max.y));
                break;
            case 1:
                result = new Vector2(Random.Range(_mapBounds.min.x, _mapBounds.max.x), Random.Range(maxCameraPosition.y, _mapBounds.max.y));
                break;
            case 2:
                result = new Vector2(Random.Range(maxCameraPosition.x, _mapBounds.max.x), Random.Range(_mapBounds.min.y, _mapBounds.max.y));
                break;
            case 3:
                result = new Vector2(Random.Range(_mapBounds.min.x, _mapBounds.max.x), Random.Range(_mapBounds.min.y, minCameraPosition.y));
                break;
        }

        return result;
    }

    private IEnumerator SpawnEnemyWithInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            _spawnInterval = _spawnInterval > 0.5f ? _spawnInterval - 0.1f : 0.5f;
            float totalWeight = 0;

            foreach (var obj in _enemyChanceConfigs)
            {
                totalWeight += obj.Chance;
            }

            float randomValue = Random.Range(0, totalWeight);
            float cumulativeWeight = 0;

            foreach (var obj in _enemyChanceConfigs)
            {
                cumulativeWeight += obj.Chance;
                if (randomValue < cumulativeWeight)
                {
                    EnemyMovement enemyMovement = Instantiate(obj.Enemy, GetRandomSpawnPosition(), Quaternion.identity);
                    enemyMovement.Init(_playerHealth.transform, _pauseManager);
                    enemyMovement.GetComponent<EnemyHealth>().OnDeath += (int points) => { OnEnemyDeath?.Invoke(points); };
                    break;
                }
            }
        }
    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused)
        {
            Debug.Log("SPAWNER PAUSE");
            StopAllCoroutines();
        }
    }
    
    private void OnDestroy()
    {
        _pauseManager.UnRegister(this);
    }
}

[Serializable]
class EnemyChanceConfig
{
    [SerializeField]
    private EnemyMovement _enemy;
    public EnemyMovement Enemy => _enemy;

    [Range(0, 100)]
    [SerializeField]
    private float _chance;
    public float Chance => _chance;
}
