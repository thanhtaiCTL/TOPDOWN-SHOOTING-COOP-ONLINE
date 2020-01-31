using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public static Vector2 moveDir;
    public static Vector3 mouseDelta;

    private Vector3 originMousePos;

    private void OnEnable()
    {
        instance = this;
    }

    private void Start()
    {
        originMousePos = Vector3.zero;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        moveDir = new Vector2(x, y);

        mouseDelta = Vector3.zero;
        if(Input.GetMouseButtonDown(0))
        {
            originMousePos = Input.mousePosition;
        }
        else if(Input .GetMouseButton(0))
        {
            mouseDelta = Input.mousePosition - originMousePos;
            originMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            originMousePos = Vector3.zero;
        }
    }
}
