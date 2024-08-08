using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Animator _animator;

    [Header("Movement Settings")] 
    [SerializeField] float _maxSpeed;

    EnemyBehaviourRun _behaviourRun;
    EnemyBehaviourIdle _behaviourIdle;
    EnemyBehaviourAttack _behaviourAttack;


    void Start()
    {
        _behaviourRun = _animator.GetBehaviour<EnemyBehaviourRun>();
        _behaviourIdle = _animator.GetBehaviour<EnemyBehaviourIdle>();
        _behaviourAttack = _animator.GetBehaviour<EnemyBehaviourAttack>();

        InitBehaviours();
    }

    void InitBehaviours()
    {
        _behaviourRun.maxSpeed = _maxSpeed;
    }

    public void ApplyDamage()
    {
        _behaviourAttack.ApplyDamage();
    }

}
