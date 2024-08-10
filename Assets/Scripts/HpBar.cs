using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HpBar : MonoBehaviour
{
    [SerializeField] Slider _hpSlider;
    [SerializeField] Slider _delayedHpSlider;
    [SerializeField] TextMeshProUGUI _hpPointsTxt;

    [Header("Settings")]
    [SerializeField] bool _doPunchOnUpdate;
    [SerializeField] bool _doScaleDownOnDeath = true;

    Camera _cam;
    Transform _worldTarget;

    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        if (_worldTarget)
        {
            transform.position = _cam.WorldToScreenPoint(_worldTarget.position);
        }
    }

    public void Init(int maxHp, Transform toFollow)
    {
        _hpSlider.maxValue = _delayedHpSlider.maxValue = maxHp;
        _hpSlider.value = _delayedHpSlider.value = maxHp;
        _hpPointsTxt.text = "" + maxHp;

        _worldTarget = toFollow;
    }

    public void UpdateValue(int hp)
    {
        _hpSlider.value = hp;

        if (_doPunchOnUpdate)
        {
            transform.DOKill(true);
            transform.DOPunchScale(Vector3.one * 0.125f, 0.25f);
        }

        DOTween.Kill(_delayedHpSlider);
        DOVirtual.Float(_delayedHpSlider.value, hp, 0.25f, (f) =>
        {
            _delayedHpSlider.value = f;

        }).SetDelay(0.25f).SetEase(Ease.OutQuad).SetId(_delayedHpSlider).OnComplete(() =>
        {
            if (_doScaleDownOnDeath && _delayedHpSlider.value <= 0)
                transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        });

        DOTween.Kill(_hpPointsTxt);
        DOVirtual.Float(int.Parse(_hpPointsTxt.text), hp, 0.5f, (f) =>
        {
            _hpPointsTxt.text = "" + (int)f;

        }).SetEase(Ease.OutQuad).SetId(_hpPointsTxt);
    }
}
