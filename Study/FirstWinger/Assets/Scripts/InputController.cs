using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        UpdateInput();
        UpdateMouse();
    }

    private void UpdateInput()
    {
        var moveDirection = Vector3.zero;
            
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection.y = 1;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection.y = -1;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection.x = -1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection.x = 1;
        }
        
        SystemManager.Instance.Hero.ProcessInput(moveDirection);
    }

    private void UpdateMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SystemManager.Instance.Hero.Fire();
        }
    }
}
