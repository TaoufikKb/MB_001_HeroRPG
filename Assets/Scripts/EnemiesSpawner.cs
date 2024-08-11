using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] EnemySpawnRate[] _enemyPrefabs;

    [Header("Settings")]
    [SerializeField] int _maxEnemiesCount;
    [SerializeField] float _timeBetweenSpawns;
    [SerializeField] float _spawnRadius;

    GameManager _gameManager;
    int _count;
    float _spawnTime;

    void Start()
    {
        _count = 0;
        _spawnTime = Time.time - _timeBetweenSpawns;

        _gameManager = GameManager.instance;
    }


    void Update()
    {
        if (!_gameManager.isPlaying)
            return;

        if (_count < _maxEnemiesCount && Time.time > _spawnTime + _timeBetweenSpawns)
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
        Instantiate(_enemyPrefabs[index].enemyPrefab, position, Quaternion.identity, transform);
    }

    public void ClearAllEnemies()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}

[System.Serializable]
public struct EnemySpawnRate
{
    public Enemy enemyPrefab;
    [Range(0, 100)] public int spawnRatePercentage;
}
