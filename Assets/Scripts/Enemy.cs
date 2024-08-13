using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Combat combat => _combat;

    [SerializeField] EnemiesData _enemyData;
    [Space]
    [SerializeField] Combat _combat;
    [SerializeField] Animator _animator;
    [SerializeField] Collider _collider;
    [SerializeField] Transform _root;
    [SerializeField] Transform _center;


    EnemyBehaviourRun _behaviourRun;
    EnemyBehaviourAttack _behaviourAttack;
    EnemyBehaviourTakeDamage _behaviourTakeDamage;
    EnemyBehaviourDie _behaviourDie;

    void Start()
    {
        _combat.Init(_enemyData.maxHp);

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
        _behaviourDie.dieExplosionFx = _enemyData.dieExplosionFx;
        _behaviourDie.dropBoxesPrefabs = _enemyData.dropBoxes;
        _behaviourDie.dropBoxPercentage = _enemyData.dropBoxPercentage;
        _behaviourDie.dropSkillPointPrefab = _enemyData.dropSkillPointPrefab;
        _behaviourDie.dropSkillPointsCount = _enemyData.dropSkillPointsCount;

        _behaviourRun.sideSpeed = _enemyData.sideSpeed;
        _behaviourRun.minPlayerDetectionRadius = _enemyData.minPlayerDetectionRadius;
        _behaviourRun.maxPlayerDetectionRadius = _enemyData.maxPlayerDetectionRadius;
        _behaviourRun.timeBetweenAttacks = _enemyData.timeBetweenAttacks;
        _behaviourRun.minRangeToAttack = _enemyData.minRangeToAttack;

        _behaviourAttack.slashFxPrefab = _enemyData.slashFxPrefab;
        _behaviourAttack.hitFxPrefab = _enemyData.hitFxPrefab;
        _behaviourAttack.damageFeedback = _enemyData.damageFeedback;
        _behaviourAttack.damage = _enemyData.damage;
        _behaviourAttack.pushPower = _enemyData.pushPower;
        _behaviourAttack.range = _enemyData.range;
        _behaviourAttack.dotHitCone = _enemyData.dotHitCone;
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
            _behaviourTakeDamage.push = Mathf.Max(0, (push - _enemyData.takeDamagePushThreshold)) * direction;
            _animator.SetTrigger("TakeDamage");
        }
    }
}
