
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour
{
    public static Player instance;

    public new Collider collider => _collider;

    [SerializeField] TwoBoneIKConstraint _rightHandIK;
    [SerializeField] DamageFeedback _damageFeedback;
    [SerializeField] Combat _combat;
    [SerializeField] Animator _animator;
    [SerializeField] Transform _root;
    [SerializeField] Collider _collider;
    [SerializeField] Weapon[] _weapons;


    //[Header("Fight Settings")]
    //[SerializeField] float _enemyDetectionRadius;

    BehaviourRun _behaviourRun;
    BehaviourIdle _behaviourIdle;
    BehaviourStrike _behaviourStrike;
    BehaviourTakeDamage _behaviourTakeDamage;
    BehaviourDie _behaviourDie;

    Weapon _weapon;


    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _behaviourRun = _animator.GetBehaviour<BehaviourRun>();
        _behaviourIdle = _animator.GetBehaviour<BehaviourIdle>();
        _behaviourStrike = _animator.GetBehaviour<BehaviourStrike>();
        _behaviourTakeDamage = _animator.GetBehaviour<BehaviourTakeDamage>();
        _behaviourDie = _animator.GetBehaviour<BehaviourDie>();

        InitBehaviours();
        EquipWeapon(_weapons[0]);
        InitEvents();
    }
    
    void InitBehaviours()
    {
        _behaviourIdle.rightHandIK = _rightHandIK;
        _behaviourStrike.damageFeedback = _damageFeedback;
        _behaviourTakeDamage.root = _root;
        _behaviourDie.collider = _collider;
    }
    void EquipWeapon(Weapon weapon)
    {
        foreach (var item in _weapons)
        {
            item.gameObject.SetActive(false);
        }
        weapon.gameObject.SetActive(true);

        _weapon = weapon;

        _behaviourRun.maxSpeed = _weapon.data.movementSpeed;
        _behaviourIdle.enemyDetectionRadius = _weapon.data.range;
        _behaviourStrike.weapon = _weapon;

        _animator.SetFloat("AttackSpeed",_weapon.data.attackSpeed);
        _animator.SetFloat("MovementSpeed", _weapon.data.movementSpeed);
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

    public void ApplyDamage()
    {
        _behaviourStrike.ApplyDamage();
    }

    public void TakeDamage(int damage, Vector3 push)
    {
        if (_combat.isDeath)
            return;

        _combat.TakeDamage(damage, out bool isDeath);

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
