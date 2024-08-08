using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData data => _data;

    [SerializeField] WeaponData _data;
}
