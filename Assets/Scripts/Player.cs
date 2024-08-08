using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Weapon _weapon;


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

        EquipWeapon();
        InitEvents();
    }
    
    void EquipWeapon()
    {
        _behaviourRun.maxSpeed = _weapon.movementSpeed;
        _behaviourIdle.enemyDetectionRadius = _weapon.range;
        _behaviourStrike.weapon = _weapon;

        _animator.SetFloat("AttackSpeed",_weapon.attackSpeed);
    }

    void InitEvents()
    {
        Joystick joystick = Joystick.instance;
        joystick.onJoystickDrag.AddListener(() => 
        {
            _animator.SetFloat("DirectionX", joystick.direction.x);
            _animator.SetFloat("DirectionZ", joystick.direction.y);
        });

        joystick.onJoystickUp.AddListener(() =>
        {
            _animator.SetFloat("DirectionX", 0);
            _animator.SetFloat("DirectionZ", 0);
        });
    }


    void Update()
    {

    }

    public void ApplyDamage()
    {
        _behaviourStrike.ApplyDamage();
    }

}
