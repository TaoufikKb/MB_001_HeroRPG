using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator _animator;

    [Header("Movement Settings")]
    [SerializeField] float _maxSpeed;

    Joystick _joystick;

    Vector3 _velocity;

    void Start()
    {
        _joystick = Joystick.instance;
    }

    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        Vector3 direction = new Vector3(_joystick.direction.x, 0, _joystick.direction.y);

        _velocity = direction * _maxSpeed;

        transform.forward = transform.forward * 0.001f + _velocity;
        transform.position += _velocity * Time.deltaTime;

        _animator.SetBool("IsRunning", _velocity.magnitude > 0);
    }
}
