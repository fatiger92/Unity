using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    private Rigidbody2D ballRB2D;
    private float moveVelocity = 5f;
    void Start()
    {
        ballRB2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            ballRB2D.velocity = transform.right * moveVelocity;
    }
}
