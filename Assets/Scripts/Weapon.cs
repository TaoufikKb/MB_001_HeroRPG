using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float range => _range;
    public int damage => _damage;
    public float attackSpeed => _attackSpeed;
    public float movementSpeed => _movementSpeed;

    [Header("Settings")]
    [SerializeField] int _damage;
    [SerializeField] float _range;
    [SerializeField] float _attackSpeed;
    [SerializeField] float _movementSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
