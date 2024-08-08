using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourStrike : StateMachineBehaviour
{
    public Weapon weapon { get; set; }
    public Transform weaponSlot { get; set; }
    public Collider targetCollider { get; set; }

    Transform _transform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        Vector3 diff = targetCollider.transform.position - _transform.position;
        diff.y = 0;

        //Vector3 targetPosition = _transform.position + diff.normalized * Mathf.Max(0, diff.magnitude - weapon.range);
        Vector3 targetPosition = _transform.position + diff.normalized * Mathf.Max(0, diff.magnitude - targetCollider.bounds.extents.magnitude);
        Quaternion targetRotation = Quaternion.LookRotation(diff);

        float duration = Mathf.Min(stateInfo.length, 0.25f);
        _transform.DOKill();
        _transform.DOMove(targetPosition, duration).SetEase(Ease.OutQuad);
        _transform.DORotateQuaternion(targetRotation, duration).SetEase(Ease.OutQuad);

        weaponSlot.DOKill();
        weaponSlot.DOScale(1,duration).SetEase(Ease.OutBack);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("IsRunning"))
            return;

        if (animator.GetFloat("DirectionX") != 0 || animator.GetFloat("DirectionZ") != 0)
        {
            animator.SetBool("IsRunning", true);
            _transform.DOKill();
            targetCollider = null;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetCollider = null;

        weaponSlot.DOKill();
        weaponSlot.DOScale(0, 0.25f).SetEase(Ease.InBack);
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

    public void ApplyDamage()
    {
        WeaponData weaponData = weapon.data;
        Destroy(Instantiate(weaponData.slashFxPrefab, _transform.position + Vector3.up, _transform.rotation), 1);

        Collider[] enemiesColliders = Physics.OverlapSphere(_transform.position, weaponData.range, LayerMask.GetMask("Enemy"));

        foreach (var enemy in enemiesColliders)
        {
            Vector3 diff = enemy.transform.position - _transform.position;
            diff.y = 0;

            if (Vector3.Dot(_transform.forward, diff.normalized) > 0)
            {
                enemy.GetComponent<Enemy>().TakeDamage(weaponData.damage, diff.normalized * Mathf.Max(weaponData.range*0.7f- diff.magnitude, 0));

                Destroy(Instantiate(weaponData.hitFxPrefab, enemy.transform.position + Vector3.up, _transform.rotation), 1);
            }
        }
    }
}
