
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] Animator _animator;
    [SerializeField] Weapon[] _weapons;


    //[Header("Fight Settings")]
    //[SerializeField] float _enemyDetectionRadius;

    BehaviourRun _behaviourRun;
    BehaviourIdle _behaviourIdle;
    BehaviourStrike _behaviourStrike;

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

        EquipWeapon(_weapons[0]);
        InitEvents();
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


    void Update()
    {

    }

    public void ApplyDamage()
    {
        _behaviourStrike.ApplyDamage();
    }

}
