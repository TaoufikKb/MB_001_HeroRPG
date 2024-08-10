using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    void Awake()
    {
        instance = this;
    }

    public void DoCameraShake(float amplitude,float frequency,float duration)
    {

    }

    public void UpdateCameraDistance(float distance)
    {

    }
}
