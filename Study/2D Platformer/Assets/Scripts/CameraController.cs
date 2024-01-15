using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public bool freezeVertical, freezeHorizontal;
    Vector3 positionStore;

    public bool clampPosition;
    public Transform clampMin, clampMax;
    float halfWidth, halfheight;
    public Camera theCam;
    
    void Start()
    {
        positionStore = transform.position;
         clampMin.SetParent(null);
         clampMax.SetParent(null);

         halfheight = theCam.orthographicSize;
         halfWidth = theCam.orthographicSize * theCam.aspect; // 높이 / 9(화면비) * 16(화면비)
    }

    // 이렇게 하는 이유는 카메라가 Update에 있으면 덜컹덜컹 거리는 구간이 생김
    // 따라서 플레이어 Update문 모두 실행되고 나서 카메라를 움직이면 부드럽게 됨
    void LateUpdate() 
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        if (freezeVertical == true)
            transform.position = new Vector3(transform.position.x, positionStore.y, transform.position.z);

        if (freezeHorizontal == true)
            transform.position = new Vector3(positionStore.x, transform.position.y, transform.position.z);

        if (clampPosition == true)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, clampMin.position.x + halfWidth,clampMax.position.x - halfWidth),
                Mathf.Clamp(transform.position.y, clampMin.position.y + halfheight,clampMax.position.y - halfheight),
                transform.position.z);
        }
    }

    void OnDrawGizmos()
    {
        if (clampPosition == true)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));
            
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));
        }
    }
}
