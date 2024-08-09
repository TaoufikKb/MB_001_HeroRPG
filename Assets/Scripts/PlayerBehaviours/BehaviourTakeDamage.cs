using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTakeDamage : StateMachineBehaviour
{
    public Transform root { get; set; }
    public Vector3 push { get; set; }

    Transform _transform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;

        float duration = Mathf.Min(stateInfo.length, 0.25f);

        root.DOKill(true);
        root.DOPunchScale(Vector3.one * 0.25f, duration).SetEase(Ease.OutQuad);

        _transform.DOKill();
        _transform.DOMove(_transform.position + push, duration).SetEase(Ease.OutQuad);

        _transform.forward = -push;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
}
