using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BehaviourIdle : StateMachineBehaviour
{
    public Joystick joystick { get; set; }
    public float enemyDetectionRadius { get; set; }

    Transform _transform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform=animator.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.IsInTransition(layerIndex))
            return;

        if (joystick.direction.magnitude > 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            Collider[] enemiesColliders = Physics.OverlapSphere(_transform.position, enemyDetectionRadius, LayerMask.GetMask("Enemy"));
            if (enemiesColliders.Length > 0)
            {
                animator.GetBehaviour<BehaviourStrike>().targetCollider = enemiesColliders.OrderBy(c => Vector3.Distance(c.transform.position, _transform.position)).FirstOrDefault();
                animator.SetTrigger("Strike");
            }
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
