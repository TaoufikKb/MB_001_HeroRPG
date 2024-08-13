using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemiesData", order = 1)]
public class EnemiesData : ScriptableObject
{
    public DamageFeedback damageFeedback => _damageFeedback;
    public GameObject dropSkillPointPrefab => _dropSkillPointPrefab;
    public GameObject[] dropBoxes => _dropBoxes;
    public GameObject slashFxPrefab => _slashFxPrefab;
    public GameObject hitFxPrefab => _hitFxPrefab;
    public GameObject dieExplosionFx => _dieExplosionFx;
    public float sideSpeed => _sideSpeed;
    public float minPlayerDetectionRadius => _minPlayerDetectionRadius;
    public float maxPlayerDetectionRadius => _maxPlayerDetectionRadius;
    public float timeBetweenAttacks => _timeBetweenAttacks;
    public float pushPower => _pushPower;
    public float range => _range;
    public float minRangeToAttack => _minRangeToAttack;
    public float dotHitCone => _dotHitCone;
    public float takeDamagePushThreshold => _takeDamagePushThreshold;
    public int maxHp => _maxHp;
    public int damage => _damage;
    public int dropSkillPointsCount => _dropSkillPointsCount;
    public int dropBoxPercentage => _dropBoxPercentage;


    [SerializeField] DamageFeedback _damageFeedback;
    [SerializeField] GameObject _dropSkillPointPrefab;
    [SerializeField] GameObject[] _dropBoxes;

    [Space]
    [SerializeField] GameObject _slashFxPrefab;
    [SerializeField] GameObject _hitFxPrefab;
    [SerializeField] GameObject _dieExplosionFx;

    [Header("Movement Settings")]
    [SerializeField] float _sideSpeed;
    [Space]
    [SerializeField] float _minPlayerDetectionRadius;
    [SerializeField] float _maxPlayerDetectionRadius;

    [Header("Combat Settings")]
    [SerializeField] int _maxHp;
    [Space]
    [SerializeField] float _timeBetweenAttacks;
    [SerializeField] int _damage;
    [SerializeField] float _pushPower;
    [SerializeField] float _range;
    [SerializeField] float _minRangeToAttack;
    [SerializeField] float _dotHitCone;
    [Space]
    [SerializeField] float _takeDamagePushThreshold;
    [SerializeField] int _dropSkillPointsCount;
    [SerializeField, Range(0, 100)] int _dropBoxPercentage;


}

