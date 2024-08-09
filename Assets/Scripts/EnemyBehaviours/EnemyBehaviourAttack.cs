using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviourAttack : StateMachineBehaviour
{
    public DamageFeedback damageFeedback { get; set; }
    public GameObject slashFxPrefab { get; set; }
    public GameObject hitFxPrefab { get; set; }
    public float range { get; set; }
    public float dotHitCone { get; set; }
    public int damage { get; set; }

    Transform _transform;
    Collider _targetCollider;

    Vector3 _startPos;
    Vector3 _endPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _targetCollider = Player.instance.collider;
        _transform = animator.transform;

        Vector3 diff = _targetCollider.transform.position - _transform.position;
        diff.y = 0;

        //Vector3 targetPosition = _transform.position + diff.normalized * Mathf.Max(0, diff.magnitude - weapon.range);
        _startPos = _transform.position;
        _endPosition = _transform.position + diff.normalized * Mathf.Max(0, diff.magnitude - _targetCollider.bounds.extents.magnitude);
        _endPosition.y = _targetCollider.transform.position.y;

        Quaternion targetRotation = Quaternion.LookRotation(diff);

        float duration = Mathf.Min(stateInfo.length * 0.25f, 0.25f);
        _transform.DOKill();
        _transform.DORotateQuaternion(targetRotation, duration).SetEase(Ease.OutQuad);


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform.position = Vector3.Lerp(_startPos, _endPosition, animator.GetFloat("AttackCurve"));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    public void ApplyDamage()
    {
        GameObject slashObj = Instantiate(slashFxPrefab, _transform.position + Vector3.up, _transform.rotation);
        slashObj.transform.localScale = range * Vector3.one;
        Destroy(slashObj, 1);

        Vector3 diff = _targetCollider.transform.position - _transform.position;
        diff.y = 0;

        if (Vector3.Dot(_transform.forward, diff.normalized) > dotHitCone)
        {
            int dmg = Mathf.Max(0, Random.Range(damage - 1, damage + 2));
            Player.instance.TakeDamage(dmg, diff.normalized * Mathf.Max(range * 0.7f - diff.magnitude, 0));

            Destroy(Instantiate(damageFeedback).Init(dmg, _targetCollider.transform.position + Vector3.up + Random.onUnitSphere * 0.5f), 1);


            GameObject hitObj = Instantiate(hitFxPrefab, _targetCollider.transform.position + Vector3.up, _transform.rotation);
            hitObj.transform.localScale = _targetCollider.bounds.size;
            Destroy(hitObj, 1);
        }
    }
}
