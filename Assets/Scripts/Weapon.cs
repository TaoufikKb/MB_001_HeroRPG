using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float range => _range;
    public int damage => _damage;

    [Header("Settings")]
    [SerializeField] int _damage;
    [SerializeField] float _range;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
