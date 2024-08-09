using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Combat _combat;
    [SerializeField] DamageFeedback _damageFeedback;
    [SerializeField] Animator _animator;
    [SerializeField] Collider _collider;
    [SerializeField] Transform _root;

    [Space]
    [SerializeField] GameObject _slashFxPrefab;
    [SerializeField] GameObject _hitFxPrefab;

    [Header("Movement Settings")] 
    [SerializeField] float _sideSpeed;
    [Space]
    [SerializeField] float _minPlayerDetectionRadius;
    [SerializeField] float _maxPlayerDetectionRadius;

    [Header("Combat Settings")]
    [SerializeField] float _timeBetweenAttacks;
    [SerializeField] int _damage;
    [SerializeField] float _range;
    [SerializeField] float _minRangeToAttack;
    [SerializeField] float _dotHitCone;

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
    }    

    void InitBehaviours()
    {
        _behaviourTakeDamage.root = _root;

        _behaviourDie.collider = _collider;

        _behaviourRun.sideSpeed = _sideSpeed;
        _behaviourRun.minPlayerDetectionRadius = _minPlayerDetectionRadius;
        _behaviourRun.maxPlayerDetectionRadius = _maxPlayerDetectionRadius;
        _behaviourRun.timeBetweenAttacks = _timeBetweenAttacks;
        _behaviourRun.minRangeToAttack = _minRangeToAttack;

        _behaviourAttack.slashFxPrefab = _slashFxPrefab;
        _behaviourAttack.hitFxPrefab = _hitFxPrefab;
        _behaviourAttack.damageFeedback = _damageFeedback;
        _behaviourAttack.damage = _damage;
        _behaviourAttack.range = _range;
        _behaviourAttack.dotHitCone = _dotHitCone;
    }


    public void ApplyDamage()
    {
        _behaviourAttack.ApplyDamage();
    }

    public void TakeDamage(int damage,Vector3 push)
    {
        if (_combat.isDeath)
            return;

        _combat.TakeDamage(damage,out bool isDeath);

        if (isDeath)
        {
            _behaviourDie.push = push * 5;
            _animator.SetTrigger("Die");
        }
        else
        {
            _behaviourTakeDamage.push = push;
            _animator.SetTrigger("TakeDamage");
        }
    }
}
