using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _damageText;

    public GameObject Init(int damage, Vector3 worldPosition)
    {

        _damageText.text = "" + damage;
        _damageText.transform.position = Camera.main.WorldToScreenPoint(worldPosition);

        return gameObject;
    }
}
