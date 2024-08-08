using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyBehaviourRun : StateMachineBehaviour
{
    public bool runForward { get; set; }

    public float minPlayerDetectionRadius { get; set; }
    public float maxPlayerDetectionRadius { get; set; }

    public float forwardSpeed { get; set; }
    public float backwardSpeed { get; set; }
    public float sideSpeed { get; set; }

    Transform _transform;
    Transform _player;

    float _randomSide;
    float _randomY;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _player = Player.instance.transform;

        _randomSide = Random.Range(0, 2) == 0 ? 1 : -1;
        _randomY = Random.Range(0f, 2f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 diff = _transform.position- _player.transform.position;
        diff.y = 0;
        diff = Quaternion.Euler(Mathf.Rad2Deg * sideSpeed * Time.deltaTime * _randomSide *_randomY * Vector3.up) * diff;

        Vector3 targetPos = _player.position + diff.normalized * Mathf.Clamp(diff.magnitude, minPlayerDetectionRadius, maxPlayerDetectionRadius) + Vector3.up * _randomY;

        _transform.position = Vector3.Lerp(_transform.position, targetPos, 0.1f);
        _transform.forward = -diff;
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
