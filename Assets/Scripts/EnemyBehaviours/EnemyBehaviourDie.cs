using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourDie : StateMachineBehaviour
{
    public GameObject[] dropBoxes { get; set; }
    public GameObject dieExplosionFx { get; set; }
    public Transform center { get;  set; }
    public Collider collider { get; set; }
    public Vector3 push { get; set; }

    Transform _transform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;

        Vector3 targetPosition = _transform.position;
        targetPosition.y = 0;

        collider.enabled = false;

        _transform.DOKill();
        _transform.DOMove(targetPosition + push, 0.25f).SetEase(Ease.OutQuart);

        DOVirtual.DelayedCall(stateInfo.length, () =>
        {
            Destroy(Instantiate(dieExplosionFx, center.position, dieExplosionFx.transform.rotation), 1);
            Instantiate(dropBoxes[Random.Range(0, dropBoxes.Length)], center.position, _transform.rotation, Level.dropBoxesHolder);

            _transform.DOKill();
            Destroy(animator.gameObject);
        });

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
