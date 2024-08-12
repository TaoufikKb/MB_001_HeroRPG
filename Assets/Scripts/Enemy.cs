using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Combat combat => _combat;

    [SerializeField] Combat _combat;
    [SerializeField] DamageFeedback _damageFeedback;
    [SerializeField] Animator _animator;
    [SerializeField] Collider _collider;
    [SerializeField] Transform _root;
    [SerializeField] Transform _center;

    [Space]
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
    [SerializeField] float _timeBetweenAttacks;
    [SerializeField] int _damage;
    [SerializeField] float _pushPower;
    [SerializeField] float _range;
    [SerializeField] float _minRangeToAttack;
    [SerializeField] float _dotHitCone;
    [Space]
    [SerializeField] float _takeDamagePushThreshold;
    [SerializeField] int _dropSkillPointsCount;
    [SerializeField,Range(0,100)] int _dropBoxPercentage;

    EnemyBehaviourRun _behaviourRun;
    EnemyBehaviourAttack _behaviourAttack;
    EnemyBehaviourTakeDamage _behaviourTakeDamage;
    EnemyBehaviourDie _behaviourDie;

    void Start()
    {
        _behaviourRun = _animator.GetBehaviour<EnemyBehaviourRun>();
        _behaviourAttack = _animator.GetBehaviour<EnemyBehaviourAttack>();
        _behaviourTakeDamage = _animator.GetBehaviour<EnemyBehaviourTakeDamage>();
        _behaviourDie = _animator.GetBehaviour<EnemyBehaviourDie>();

        InitBehaviours();

        _root.localScale = Vector3.zero;
        _root.DOKill(true);
        _root.DOScale(1, 0.25f).SetEase(Ease.OutBack);
    }    

    void InitBehaviours()
    {
        _behaviourTakeDamage.root = _root;

        _behaviourDie.center = _center;
        _behaviourDie.collider = _collider;
        _behaviourDie.dieExplosionFx = _dieExplosionFx;
        _behaviourDie.dropBoxesPrefabs = _dropBoxes;
        _behaviourDie.dropBoxPercentage = _dropBoxPercentage;
        _behaviourDie.dropSkillPointPrefab = _dropSkillPointPrefab;
        _behaviourDie.dropSkillPointsCount = _dropSkillPointsCount;

        _behaviourRun.sideSpeed = _sideSpeed;
        _behaviourRun.minPlayerDetectionRadius = _minPlayerDetectionRadius;
        _behaviourRun.maxPlayerDetectionRadius = _maxPlayerDetectionRadius;
        _behaviourRun.timeBetweenAttacks = _timeBetweenAttacks;
        _behaviourRun.minRangeToAttack = _minRangeToAttack;

        _behaviourAttack.slashFxPrefab = _slashFxPrefab;
        _behaviourAttack.hitFxPrefab = _hitFxPrefab;
        _behaviourAttack.damageFeedback = _damageFeedback;
        _behaviourAttack.damage = _damage;
        _behaviourAttack.pushPower = _pushPower;
        _behaviourAttack.range = _range;
        _behaviourAttack.dotHitCone = _dotHitCone;
        _behaviourAttack.audioManager = AudioManager.instance;
    }


    public void ApplyDamage()
    {
        _behaviourAttack.ApplyDamage();
    }

    public void TakeDamage(int damage,float push,Vector3 direction)
    {
        if (_combat.isDeath)
            return;

        _combat.TakeDamage(damage,out bool isDeath);

        if (isDeath)
        {
            _behaviourDie.push = Mathf.Max(3, push) * direction;
            _animator.SetTrigger("Die");
        }
        else
        {
            _behaviourTakeDamage.push = Mathf.Max(0, (push - _takeDamagePushThreshold)) * direction;
            _animator.SetTrigger("TakeDamage");
        }
    }
}
