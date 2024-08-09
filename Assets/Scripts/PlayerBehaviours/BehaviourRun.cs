using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using DG.Tweening;

public class BehaviourRun : StateMachineBehaviour
{
    public TwoBoneIKConstraint rightHandIK { get; set; }
    public float maxSpeed { get; set; }

    Transform _transform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;

        DOTween.Kill(rightHandIK);
        DOVirtual.Float(rightHandIK.weight, 1, 0.25f, (f) =>
        {
            rightHandIK.weight = f;

        }).SetEase(Ease.OutQuad).SetId(rightHandIK);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 direction = new Vector3(animator.GetFloat("DirectionX"), 0, animator.GetFloat("DirectionZ"));

        if (direction.magnitude > 0)
        {
            direction.Normalize();
            Vector3 forward = Vector3.Slerp(_transform.forward, direction, 0.2f);

            _transform.forward = forward;
            _transform.position += direction * maxSpeed * Time.deltaTime;
        }
        else
        {
            animator.SetBool("IsRunning", false);

        }        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DOTween.Kill(rightHandIK);
        DOVirtual.Float(rightHandIK.weight, 0, 0.25f, (f) =>
        {
            rightHandIK.weight = f;

        }).SetEase(Ease.OutQuad).SetId(rightHandIK);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}
}
