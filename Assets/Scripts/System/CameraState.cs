using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CameraStateType
{
    NORMAL = 1,
    SKILL = 2
}

[Serializable]
public class CameraState
{
    public CameraStateType stateType = CameraStateType.NORMAL;
    public Vector3 offset;
    public Vector3 angle;

    [Range(10, 100)]
    public float fov = 60;
    public float speedFlow = 5f;
}
