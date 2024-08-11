using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillPointsDrop : MonoBehaviour, ICollectible
{
    [SerializeField] GameObject _collectFx;
    [SerializeField] Collider _collider;

    bool _isCollected = false;

    void Awake()
    {
        _collider.enabled = false;
    }
    void Start()
    {
        Vector3 targetJumpPos = Random.insideUnitCircle * 5;
        targetJumpPos.z = targetJumpPos.y;
        targetJumpPos += transform.position;
        targetJumpPos.y = 0;

        float randDelay = Random.Range(0f, 0.25f);
        transform.DOJump(targetJumpPos, 5, 1, 1f).SetEase(Ease.Linear).SetDelay(randDelay);
        DOVirtual.DelayedCall(0.5f + randDelay, () => _collider.enabled = true);
    }

    public void Collect()
    {
        if (_isCollected)
            return;

        _collider.enabled = false;
        _isCollected = true;


        Player player = Player.instance;
        transform.parent = player.transform;

        transform.DOKill();
        transform.DOLocalMove(Vector3.up * 0.5f, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            Destroy(gameObject);
            Destroy(Instantiate(_collectFx, transform.position, transform.rotation), 1);
        });


    }
}
