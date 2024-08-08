using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Animator _animator;

    [Header("Movement Settings")] 
    [SerializeField] float _forwardSpeed;
    [SerializeField] float _backwardSpeed;
    [SerializeField] float _sideSpeed;
    [Space]
    [SerializeField] float _minPlayerDetectionRadiusBackward;
    [SerializeField] float _minPlayerDetectionRadiusForward;
    [SerializeField] float _maxPlayerDetectionRadiusBackward;
    [SerializeField] float _maxPlayerDetectionRadiusForward;

    EnemyBehaviourRun _behaviourRun;
    EnemyBehaviourIdle _behaviourIdle;
    EnemyBehaviourAttack _behaviourAttack;
    EnemyBehaviourTakeDamage _behaviourTakeDamage;


    void Start()
    {
        _behaviourRun = _animator.GetBehaviour<EnemyBehaviourRun>();
        _behaviourIdle = _animator.GetBehaviour<EnemyBehaviourIdle>();
        _behaviourAttack = _animator.GetBehaviour<EnemyBehaviourAttack>();
        _behaviourTakeDamage = _animator.GetBehaviour<EnemyBehaviourTakeDamage>();

        InitBehaviours();
    }

    void InitBehaviours()
    {

        _behaviourRun.forwardSpeed = _forwardSpeed;
        _behaviourRun.backwardSpeed = _backwardSpeed;
        _behaviourRun.sideSpeed = _sideSpeed;

        _behaviourRun.minPlayerDetectionRadiusForward = _minPlayerDetectionRadiusForward;
        _behaviourRun.maxPlayerDetectionRadiusBackward = _maxPlayerDetectionRadiusBackward;

        _behaviourIdle.minPlayerDetectionRadiusBackward = _minPlayerDetectionRadiusBackward;
        _behaviourIdle.maxPlayerDetectionRadiusForward = _maxPlayerDetectionRadiusForward;
    }

    public void ApplyDamage()
    {
        _behaviourAttack.ApplyDamage();
    }

    public void TakeDamage(int damage,Vector3 push)
    {
        _behaviourTakeDamage.push = push;

        _animator.SetTrigger("TakeDamage");
    }
}
