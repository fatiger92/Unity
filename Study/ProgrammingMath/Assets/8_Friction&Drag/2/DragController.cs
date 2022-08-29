using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Drag { Drag, Simulation };
public class DragController : MonoBehaviour
{
    public Drag ballMode;
    public float dragValue;
    private Rigidbody2D ballRigidbody2D;
    void Start()
    {
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballRigidbody2D.velocity = transform.right;
    }

    void Update()
    {
        if (ballMode == Drag.Simulation)
            ballRigidbody2D.velocity = ballRigidbody2D.velocity * (1 - dragValue * Time.deltaTime);

    }
}
