using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Combat : MonoBehaviour
{
    public UnityEvent onDeath { get; private set; } = new UnityEvent();
    public int currentHp {  get; private set; }
    public bool isDeath {  get; private set; }

    [SerializeField] HpBar _hpBar;
    [SerializeField] Transform _hpBarPosition;

    int _maxHp;

    void Awake()
    {
        currentHp = _maxHp;
    }
    public void Init(int maxHP)
    {
        _maxHp= maxHP;
        currentHp = _maxHp;

        _hpBar.Init(_maxHp, _hpBarPosition);

        _hpBar.transform.localScale = Vector3.zero;
        _hpBar.transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);
    }

    public void TakeDamage(int damage,out bool isDeath)
    {
        isDeath = this.isDeath;

        if (this.isDeath)
            return;

        currentHp = Mathf.Max(currentHp - damage, 0);

        _hpBar.UpdateValue(currentHp);

        if (currentHp == 0)
        {
            this.isDeath = isDeath = true;            

            onDeath?.Invoke();
        }
    }

    public void Revive()
    {
        isDeath = false;

        currentHp = _maxHp;
        _hpBar.Init(_maxHp, _hpBarPosition);

    }

    public void ResetStats()
    {
        isDeath = false;

        currentHp = _maxHp;
        _hpBar.Init(_maxHp, _hpBarPosition);
    }
}
