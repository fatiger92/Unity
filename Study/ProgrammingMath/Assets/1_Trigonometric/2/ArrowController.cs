using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D arrowRigidbody2D;
    private float arrowPower = 12f;
    void Start()
    {
        arrowRigidbody2D = GetComponent<Rigidbody2D>();
        arrowRigidbody2D.AddForce(arrowPower*transform.right,ForceMode2D.Impulse);
    }

    private void Update()
    {
        transform.right = arrowRigidbody2D.velocity.normalized;
        // 내 오른쪽 방향을 내 현재의 속도의 방향 벡터로 설정하겠다.
        // normalized -> 방향 벡터를 만든다는 뜻.
    }
}
