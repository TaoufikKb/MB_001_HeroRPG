using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public GameObject slashFxPrefab => _slashFxPrefab;
    public GameObject hitFxPrefab => _hitFxPrefab;
    public float range => _range;
    public int damage => _damage;
    public float attackSpeed => _attackSpeed;
    public float movementSpeed => _movementSpeed;

    [SerializeField] GameObject _slashFxPrefab;
    [SerializeField] GameObject _hitFxPrefab;

    [Header("Settings")]
    [SerializeField] int _damage;
    [SerializeField] float _range;
    [SerializeField] float _attackSpeed;
    [SerializeField] float _movementSpeed;

}
