using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourRun : StateMachineBehaviour
{
    public float maxSpeed { get; set; }

    Transform _transform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
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
