using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Combat : MonoBehaviour
{
    public UnityEvent onDeath {  get; private set; }
    public int currentHp {  get; private set; }
    public bool isDeath {  get; private set; }

    [SerializeField] HpBar _hpBar;
    [SerializeField] Transform _hpBarPosition;

    [Header("Settings")]
    [SerializeField] int _maxHp;
    [SerializeField] bool _doScaleDownOnDeath = true;

    void Awake()
    {
        currentHp = _maxHp;
    }
    void Start()
    {
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

            if(_doScaleDownOnDeath)
                _hpBar.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);

            onDeath?.Invoke();
        }
    }
}
