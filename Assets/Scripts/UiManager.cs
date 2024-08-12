using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] Transform _endGameUI;
    [SerializeField] Transform _startGameUI;
    [SerializeField] Image _weaponIcon;
    [SerializeField] TextMeshProUGUI _pointsTxt;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _endGameUI.gameObject.SetActive(false);
        _startGameUI.gameObject.SetActive(true);
    }

    public void UpdateWeaponIcon(WeaponData weaponData)
    {
        _weaponIcon.sprite = weaponData.uiIcon;

        _weaponIcon.transform.DOKill(true);
        _weaponIcon.transform.DOPunchScale(Vector3.one * 0.25f, 0.25f);
    }

    public void ShowEndGameUI(bool show)
    {
        _endGameUI.gameObject.SetActive(show);

    }

    public void ShowStartGameUI(bool show)
    {
        _startGameUI.gameObject.SetActive(show);
    }

    public void UpdatePointsUI(int points)
    {
        _pointsTxt.transform.DOKill(true);
        _pointsTxt.transform.DOPunchScale(Vector3.one * 0.125f, 0.25f);

        DOTween.Kill(_pointsTxt);
        DOVirtual.Float(int.Parse(_pointsTxt.text), points, 0.5f, (f) =>
        {
            _pointsTxt.text = "" + (int)f;

        }).SetEase(Ease.OutQuad).SetId(_pointsTxt);
    }
}
