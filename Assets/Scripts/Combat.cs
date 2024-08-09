using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public int currentHp {  get; private set; }

    [SerializeField] HpBar _hpBar;
    [SerializeField] Transform _hpBarPosition;

    [Header("Settings")]
    [SerializeField] int _maxHp;


    void Awake()
    {
        currentHp = _maxHp;
    }
    void Start()
    {
        _hpBar.Init(_maxHp, _hpBarPosition);
    }

    public void ApplyDamage(Combat opponent,int damage)
    {
        opponent.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Max(currentHp - damage, 0);

        _hpBar.UpdateValue(currentHp);
    }
}
