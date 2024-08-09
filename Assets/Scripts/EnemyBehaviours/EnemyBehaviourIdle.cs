using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourIdle : StateMachineBehaviour
{
    public float maxPlayerDetectionRadiusForward { get; set; }
    public float minPlayerDetectionRadiusBackward { get; set; }

    EnemyBehaviourRun _enemyBehaviourRun;
    Transform _transform;
    Player _player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _player = Player.instance;
        _enemyBehaviourRun = animator.GetBehaviour<EnemyBehaviourRun>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 diff = _player.transform.position - _transform.position;
        diff.y = 0;

        _transform.forward = diff.normalized;

        if (diff.magnitude > maxPlayerDetectionRadiusForward)
        {
            animator.SetBool("IsRunning", true);
        }
        else if (diff.magnitude < minPlayerDetectionRadiusBackward)
        {
            animator.SetBool("IsRunning", true);
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
