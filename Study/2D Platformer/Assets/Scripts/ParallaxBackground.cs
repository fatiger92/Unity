using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    Transform theCam;
    public Transform sky, treeline;
    [Range(0f, 1f)]
    public float parallaxSpeed;
    
    void Start()
    {
        // Camera.main 으로 가져오는게 안좋다고들 하는데 Start에서 딱 한번만 가져올 경우 상관없음.
        // Update에서만 안가져오면 됨.
        theCam = Camera.main.transform;
    }

    void LateUpdate()
    {
        sky.position = new Vector3(theCam.position.x, theCam.position.y, sky.position.z);
        
        // y는 대체로 적용하지 않는다.
        treeline.position = new Vector3(
            theCam.position.x * parallaxSpeed, 
            /* theCam.position.y * parallaxSpeed */
            theCam.position.y, 
            treeline.position.z);
    }
}
