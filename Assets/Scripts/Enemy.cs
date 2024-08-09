using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Combat _combat;

    [Header("Movement Settings")] 
    [SerializeField] float _sideSpeed;
    [Space]
    [SerializeField] float _minPlayerDetectionRadius;
    [SerializeField] float _maxPlayerDetectionRadius;

    EnemyBehaviourRun _behaviourRun;
    EnemyBehaviourAttack _behaviourAttack;
    EnemyBehaviourTakeDamage _behaviourTakeDamage;


    void Start()
    {
        _behaviourRun = _animator.GetBehaviour<EnemyBehaviourRun>();
        _behaviourAttack = _animator.GetBehaviour<EnemyBehaviourAttack>();
        _behaviourTakeDamage = _animator.GetBehaviour<EnemyBehaviourTakeDamage>();

        InitBehaviours();
    }

    void InitBehaviours()
    {
        _behaviourRun.sideSpeed = _sideSpeed;

        _behaviourRun.minPlayerDetectionRadius = _minPlayerDetectionRadius;
        _behaviourRun.maxPlayerDetectionRadius = _maxPlayerDetectionRadius;
    }

    public void ApplyDamage()
    {
        _behaviourAttack.ApplyDamage();
    }

    public void TakeDamage(int damage,Vector3 push)
    {
        _behaviourTakeDamage.push = push;
        _animator.SetTrigger("TakeDamage");

        _combat.TakeDamage(damage);
    }
}
