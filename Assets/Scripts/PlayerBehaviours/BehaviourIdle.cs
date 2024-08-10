using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.Animations.Rigging;

public class BehaviourIdle : StateMachineBehaviour
{
    public TwoBoneIKConstraint rightHandIK { get; set; }
    public float enemyDetectionRadius { get; set; }

    Transform _transform;
    BehaviourStrike _behaviourStrike;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _behaviourStrike = animator.GetBehaviour<BehaviourStrike>();

        DOTween.Kill(rightHandIK);
        DOVirtual.Float(rightHandIK.weight, 1, 0.25f, (f) =>
        {
            rightHandIK.weight = f;

        }).SetEase(Ease.OutQuad).SetId(rightHandIK);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat("DirectionX") != 0 || animator.GetFloat("DirectionZ") != 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else if (!_behaviourStrike.targetCollider)
        {
            Collider[] enemiesColliders = Physics.OverlapCapsule(_transform.position, _transform.position + Vector3.up * 10, enemyDetectionRadius, LayerMask.GetMask("Enemy"));
            if (enemiesColliders.Length > 0)
            {
                _behaviourStrike.targetCollider = enemiesColliders.OrderBy(c => Vector3.Distance(c.transform.position, _transform.position)).FirstOrDefault();
                animator.SetTrigger("Strike");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("IsRunning"))
        {
            DOTween.Kill(rightHandIK);
            DOVirtual.Float(rightHandIK.weight, 0, 0.25f, (f) =>
            {
                rightHandIK.weight = f;

            }).SetEase(Ease.OutQuad).SetId(rightHandIK);
        }
        
    }

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
}
