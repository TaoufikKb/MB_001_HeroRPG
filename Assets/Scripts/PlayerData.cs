using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public DamageFeedback damageFeedback => _damageFeedback;
    public float takeDamagePushThreshold => _takeDamagePushThreshold;
    public int maxHp => _maxHp;

    [SerializeField] DamageFeedback _damageFeedback;

    [Header("Fight Settings")]
    [SerializeField] float _takeDamagePushThreshold;
    [SerializeField] int _maxHp;

}

