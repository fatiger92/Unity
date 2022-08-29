using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    private Rigidbody2D rotationRB2D;
    private float movePower = 5f;
    void Start()
    {
        rotationRB2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            rotationRB2D.AddForce(transform.right * movePower, ForceMode2D.Impulse);
            // 직선 방향으로 힘을 줄 때 AddForce
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rotationRB2D.AddTorque(movePower, ForceMode2D.Impulse);
            // 회전에 대해서 힘을 가할 때 AddTorque
            // Impulse, Force
        }
    }
}
