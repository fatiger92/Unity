using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreelineMover : MonoBehaviour
{
    public float maxDistance = 22f;

    Camera theCam;
    
    void Start()
    {
        theCam = Camera.main;
    }

    void Update()
    {
        float distance = transform.position.x - theCam.transform.position.x;
        
        if(distance > maxDistance)
        {
            transform.position -= new Vector3(maxDistance * 2f, 0f, 0f);
        }

        if (distance < -maxDistance)
        {
            transform.position += new Vector3(maxDistance * 2f, 0f, 0f);
        }
    }
}
