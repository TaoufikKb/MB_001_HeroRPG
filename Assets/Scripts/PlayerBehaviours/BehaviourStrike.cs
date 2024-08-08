using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourStrike : StateMachineBehaviour
{
    public Weapon weapon { get; set; }
    public Collider targetCollider { get; set; }

    Transform _transform;
    Vector3 _targetPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        Vector3 diff = targetCollider.transform.position - _transform.position;

        _targetPosition = _transform.position + diff.normalized * Mathf.Max(0, diff.magnitude - targetCollider.bounds.extents.magnitude);
        //_targetPosition = _transform.position + diff.normalized * Mathf.Max(0, diff.magnitude - weapon.range);

        _transform.DOKill();
        _transform.DOMove(_targetPosition, 0.25f).SetEase(Ease.OutQuad);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 diff = targetCollider.transform.position - _transform.position;
        diff.y = 0;
        _transform.forward = diff;
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
        Collider[] enemiesColliders = Physics.OverlapSphere(_transform.position, weapon.range, LayerMask.GetMask("Enemy"));

        foreach (var enemy in enemiesColliders)
        {
            Vector3 diff = enemy.transform.position - _transform.position;
            diff.y = 0;
            diff.Normalize();

            if (Vector3.Dot(_transform.forward, diff) > 0)
            {
                //enemy.GetComponent<Enemy>().GetDamage();
                enemy.transform.DOKill();
                enemy.transform.DOMove(_transform.position + diff * weapon.range, 0.25f).SetEase(Ease.OutQuad);
            }
        }


    }
}
