using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    public static Joystick instance;

    public Vector2 direction {  get; private set; }

    [SerializeField] Image _joystickImage;
    [SerializeField] Image _innerStickImage;
    [SerializeField] Transform _innerStick;

    [Header("Settings")]
    [SerializeField] float _joystickRadius;

    void Awake()
    {
        instance = this;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.position = Input.mousePosition;

            _innerStick.localPosition = Vector3.zero;
            direction = Vector2.zero;

            _joystickImage.enabled = true;
            _innerStickImage.enabled = true;
        }

        else if (Input.GetMouseButton(0))
        {
            Vector3 localPos = transform.InverseTransformPoint(Input.mousePosition);
            localPos = Vector3.ClampMagnitude(localPos, _joystickRadius);

            _innerStick.localPosition = localPos;

            direction = localPos / _joystickRadius;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            _innerStick.localPosition = Vector3.zero;
            direction = Vector2.zero;

            _joystickImage.enabled = false;
            _innerStickImage.enabled = false;
        }
    }
}
