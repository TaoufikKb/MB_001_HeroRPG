using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] EnemySpawnRate[] _enemyPrefabs;

    [Header("Settings")]
    [SerializeField] int _maxEnemiesCount;
    [SerializeField] float _enemiesCountIncreasePerKill;
    [SerializeField] float _timeBetweenSpawns;
    [SerializeField] float _spawnRadius;

    GameManager _gameManager;
    float _maxCount;
    int _count;
    float _spawnTime;

    void Start()
    {
        _count = 0;
        _maxCount = 1;
        _spawnTime = Time.time;

        _gameManager = GameManager.instance;
    }


    void Update()
    {
        if (!_gameManager.isPlaying)
        {
            _spawnTime = Time.time;
            return;
        }

        if (_count < _maxCount && Time.time > _spawnTime + _timeBetweenSpawns)
        {
            _spawnTime = Time.time;

            SpawnEnemy();
            _count++;
        }
    }

    void SpawnEnemy()
    {
        int rand = Random.Range(1, 101);
        int index = 0;
        while (rand > _enemyPrefabs[index].spawnRatePercentage)
        {
            rand -= _enemyPrefabs[index].spawnRatePercentage;
            index++;
        }

        Vector3 randVect = Random.onUnitSphere;
        randVect.y = 0;
        randVect.Normalize();

        Vector3 position = Player.instance.transform.position + randVect * _spawnRadius;

        Enemy enemy = Instantiate(_enemyPrefabs[index].enemyPrefab, position, Quaternion.identity, transform);

        enemy.combat.onDeath.AddListener(() => 
        {
            _count--;
            _maxCount = Mathf.Min(_maxEnemiesCount, _maxCount + _enemiesCountIncreasePerKill);
        });
    }

    public void ClearAllEnemies()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void KillAllEnemies()
    {
        Enemy[] enemies = GetComponentsInChildren<Enemy>();

        foreach (var enemy in enemies)
        {
            enemy.TakeDamage(9999, 6, -enemy.transform.forward);
        }

        _spawnTime = Time.time + 3;
    }
}

[System.Serializable]
public struct EnemySpawnRate
{
    public Enemy enemyPrefab;
    [Range(0, 100)] public int spawnRatePercentage;
}
