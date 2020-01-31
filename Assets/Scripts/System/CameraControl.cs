using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraControl : MonoBehaviour
{
    public CameraState currentState;
    public CameraState[] cameraStates;
    public Transform target;
    [NonSerialized]
    public Transform trans;
    public float speedRotate = 3f;

    private Dictionary<CameraStateType, CameraState> dicCamera = new Dictionary<CameraStateType, CameraState>();
    private Camera cam;

    private void Start()
    {
        cam = gameObject.GetComponentInChildren<Camera>();
        trans = transform;
        foreach(CameraState e in cameraStates)
        {
            dicCamera.Add(e.stateType, e);
        }
        ChangeState(CameraStateType.NORMAL);
    }

    public void ChangeState(CameraStateType newState)
    {
        currentState = dicCamera[newState];
    }

    private void LateUpdate()
    {
        if(target != null)
        {
            trans.position = Vector3.Lerp(trans.position, target.position, Time.deltaTime * currentState.speedFlow);
            Quaternion quaternion = Quaternion.Euler(currentState.angle);
            cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, quaternion, Time.deltaTime * 6);

            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, currentState.fov, Time.deltaTime * 6);
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, currentState.offset, Time.deltaTime * 6);

            trans.forward = Vector3.Lerp(trans.forward, target.forward, Time.deltaTime * 10);
        }
    }
}
