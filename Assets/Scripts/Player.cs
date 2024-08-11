
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour
{
    public static Player instance;

    public WeaponData currentWeaponData => _weapon.data;
    public new Collider collider => _collider;

    [SerializeField] TwoBoneIKConstraint _rightHandIK;
    [SerializeField] DamageFeedback _damageFeedback;
    [SerializeField] Combat _combat;
    [SerializeField] Animator _animator;
    [SerializeField] Transform _weaponSlot;
    [SerializeField] Transform _root;
    [SerializeField] Collider _collider;
    [SerializeField] Weapon[] _weapons;

    [Space]
    [SerializeField] ParticleSystem _collectWeapnFx;
    [SerializeField] ParticleSystem _weapnSpawnFx;

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
        _behaviourIdle.enemyDetectionRadius = Mathf.Max(3, _weapon.data.range);
        _behaviourStrike.weapon = _weapon;

        _animator.SetFloat("AttackSpeed", _weapon.data.attackSpeed);
        _animator.SetFloat("MovementSpeed", _weapon.data.movementSpeed);

        _weaponSlot.localScale = Vector3.zero;
        _weaponSlot.DOKill();
        _weaponSlot.DOScale(_weapon.data.range, 0.5f).SetEase(Ease.OutBack);

        UiManager.instance.UpdateWeaponIcon(_weapon.data);
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
            _behaviourDie.push = 5 * push;
            _animator.SetTrigger("Die");
        }
        else
        {
            _behaviourTakeDamage.push = push;
            _animator.SetTrigger("TakeDamage");
        }
    }

    public void CollectWeapon(WeaponData weaponData)
    {
        foreach (var weapon in _weapons)
        {
            if (weapon.data == weaponData)
            {
                //Destroy(Instantiate(_collectWeapnFx, transform.position, transform.rotation), 1);
                _collectWeapnFx.Stop();
                _collectWeapnFx.Play();

                _weapnSpawnFx.Stop();
                _weapnSpawnFx.Play();

                EquipWeapon(weapon);

                return;
            }
        }
    }

    public void Revive()
    {
        _animator.SetTrigger("Revive");

        _combat.Revive();
    }

    public void ResetStats()
    {
        _animator.SetTrigger("Revive");

        EquipWeapon(_weapons[Random.Range(0, _weapons.Length)]);

        _combat.ResetStats();
    }
}
