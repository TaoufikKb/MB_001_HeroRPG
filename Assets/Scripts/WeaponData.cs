using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public WeaponMaterialType weaponMaterialType => _weaponMaterialType;
    public Sprite uiIcon => _uiIcon;
    public GameObject slashFxPrefab => _slashFxPrefab;
    public GameObject hitFxPrefab => _hitFxPrefab;
    public float range => _range;
    public float dotHitCone => _dotHitCone;
    public int damage => _damage;
    public float push => _push;
    public float attackSpeed => _attackSpeed;
    public float movementSpeed => _movementSpeed;

    [SerializeField] WeaponMaterialType _weaponMaterialType;
    [SerializeField] Sprite _uiIcon;
    [SerializeField] GameObject _slashFxPrefab;
    [SerializeField] GameObject _hitFxPrefab;

    [Header("Settings")]
    [SerializeField] int _damage;
    [SerializeField] float _range;
    [SerializeField] float _push;
    [SerializeField] float _dotHitCone;
    [SerializeField] float _attackSpeed;
    [SerializeField] float _movementSpeed;

}

public enum WeaponMaterialType { Metal,Wood}
