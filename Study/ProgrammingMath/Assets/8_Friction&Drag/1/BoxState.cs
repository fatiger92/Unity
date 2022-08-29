using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxState : MonoBehaviour
{
    public PhysicsMaterial2D slopeMaterial;
    private Rigidbody2D boxRigidbody2D;

    private float boxMass = 0f;
    private float gravity = 0f;
    private float friction = 0f; // 마찰계수
    private float angle = 0f;

    void Start()
    {
        boxRigidbody2D = GetComponent<Rigidbody2D>();
        boxMass = boxRigidbody2D.mass; // 질량값을 받음
        gravity = 9.8f * boxRigidbody2D.gravityScale; // 중력값
        friction = slopeMaterial.friction; // 마찰계수
        angle = transform.rotation.eulerAngles.z; // 기본적으로 쿼터니언으로 각도를 받기 떄문에
        // 쿼터니언을 오일러각으로 변경

        float pushForce = boxMass * gravity * Mathf.Sin(angle * Mathf.Deg2Rad);
        float frictionForce = friction * boxMass * gravity * Mathf.Cos(angle * Mathf.Deg2Rad);

        Debug.Log("Push: " + pushForce + " , Friction: " + frictionForce);

        if (pushForce > frictionForce)
            Debug.Log("움직임");
        else
            Debug.Log("정지");
    }
}
