using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] Enemy _enemyPrefab;

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
        _spawnTime = Time.time- _timeBetweenSpawns;

        _gameManager = GameManager.instance;
    }


    void Update()
    {
        if (!_gameManager.isPlaying)
            return;

        if (_count <_maxEnemiesCount && Time.time > _spawnTime+_timeBetweenSpawns)
        {
            _spawnTime = Time.time;

            SpawnEnemy();
            _count++;
        }
    }

    void SpawnEnemy()
    {
        Vector3 rand = Random.onUnitSphere;
        rand.y = 0;
        rand.Normalize();

        Vector3 position = Player.instance.transform.position + rand * _spawnRadius;
        Instantiate(_enemyPrefab, position, Quaternion.identity, transform);
    }

    public void ClearAllEnemies()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
