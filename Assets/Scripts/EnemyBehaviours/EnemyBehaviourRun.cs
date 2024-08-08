using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourRun : StateMachineBehaviour
{
    public bool runForward { get; set; }

    public float minPlayerDetectionRadiusForward { get; set; }
    public float maxPlayerDetectionRadiusBackward { get; set; }

    public float forwardSpeed { get; set; }
    public float backwardSpeed { get; set; }
    public float sideSpeed { get; set; }

    Transform _transform;
    Transform _player;

    float _randomSide;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _player = Player.instance.transform;

        _randomSide = Random.Range(0, 2) == 0 ? 1 : -1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("IsRunning"))
            return;

        Vector3 diff = _player.position - _transform.position;
        diff.y = 0;

        _transform.RotateAround(_player.position, _randomSide * Vector3.up, Mathf.Rad2Deg * sideSpeed * Time.deltaTime / diff.magnitude);
        //_transform.forward = Vector3.Slerp(_transform.forward, diff.normalized, 0.2f);
        _transform.forward = diff.normalized;

        if (runForward)
        {
            if (diff.magnitude > minPlayerDetectionRadiusForward)
            {
                _transform.position += diff.normalized * forwardSpeed * Time.deltaTime;
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
        }
        else
        {
            if (diff.magnitude < maxPlayerDetectionRadiusBackward)
            {
                _transform.position -= diff.normalized * backwardSpeed * Time.deltaTime;
            }
            else
            {
                animator.SetBool("IsRunning", false);
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
