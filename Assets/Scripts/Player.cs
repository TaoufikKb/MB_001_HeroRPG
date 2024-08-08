using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Weapon _weapon;

    [Header("Movement Settings")]
    [SerializeField] float _maxSpeed;

    //[Header("Fight Settings")]
    //[SerializeField] float _enemyDetectionRadius;

    BehaviourRun _behaviourRun;
    BehaviourIdle _behaviourIdle;
    BehaviourStrike _behaviourStrike;


    void Start()
    {
        _behaviourRun = _animator.GetBehaviour<BehaviourRun>();
        _behaviourIdle = _animator.GetBehaviour<BehaviourIdle>();
        _behaviourStrike = _animator.GetBehaviour<BehaviourStrike>();

        InitBehaviours();
    }

    void InitBehaviours()
    {
        _behaviourRun.joystick = Joystick.instance;
        _behaviourRun.maxSpeed = _maxSpeed;

        _behaviourIdle.joystick = Joystick.instance;
        _behaviourIdle.enemyDetectionRadius = _weapon.range;

        _behaviourStrike.weapon = _weapon;
    }

    void Update()
    {

    }

    public void ApplyDamage()
    {
        _behaviourStrike.ApplyDamage();
    }

}
