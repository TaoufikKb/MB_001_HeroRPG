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

        _worldTarget= toFollow;
    }

    public void UpdateValue(int hp)
    {
        _hpSlider.value = hp;

        DOTween.Kill(_delayedHpSlider);
        DOVirtual.Float(_delayedHpSlider.value, hp, 0.25f, (f) =>
        {
            _delayedHpSlider.value = f;

        }).SetDelay(0.25f).SetEase(Ease.OutQuad).SetId(_delayedHpSlider);

        DOTween.Kill(_hpPointsTxt);
        DOVirtual.Float(int.Parse(_hpPointsTxt.text), hp, 0.5f, (f) =>
        {
            _hpPointsTxt.text = "" + (int)f;

        }).SetEase(Ease.OutQuad).SetId(_hpPointsTxt);
    }
}
