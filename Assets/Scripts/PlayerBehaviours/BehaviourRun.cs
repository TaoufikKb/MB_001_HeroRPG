using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourRun : StateMachineBehaviour
{
    public Joystick joystick { get; set; }
    public Transform transform { get; set; }
    public float maxSpeed { get; set; }


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 direction = new Vector3(joystick.direction.x, 0, joystick.direction.y);

        if (direction.magnitude > 0)
        {
            direction.Normalize();
            Vector3 forward = Vector3.Slerp(transform.forward, direction, 0.2f);

            transform.forward = forward;
            transform.position += direction * maxSpeed * Time.deltaTime;
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
