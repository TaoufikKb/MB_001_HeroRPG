using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] Image _weaponIcon;

    void Awake()
    {
        instance = this;
    }

    public void UpdateWeaponIcon(WeaponData weaponData)
    {
        _weaponIcon.sprite = weaponData.uiIcon;

        _weaponIcon.transform.DOKill(true);
        _weaponIcon.transform.DOPunchScale(Vector3.one * 0.25f, 0.25f);
    }
}
