using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController2 : MonoBehaviour
{
    public GameObject bulletObject;
    public Transform bulletContainer;
    public GameObject guideLine;
    
    public float ditectionRange = 4f;


    private Camera mainCamera;
    
    // 마우스의 위치에 따라서 가이드 라인이 생기고 미사일이 발사
    // 카메라 위에 마우스의 좌표가 어떻게 나오는지 알기 위해서
    
    void Start()
    {
        mainCamera = Camera.main;
        // 현재 사용하고 있는 카메라 객체가 들어가게 됨
    }

    void Update()
    {
        MouseCheck();
        
        if (Input.GetMouseButtonDown(0)) // 마우스의 왼쪽 버튼을 누르면
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = mainCamera.ScreenToWorldPoint(mousePos);

            Vector3 playerPos = transform.position;
            
            Vector2 dirVec = mousePos - (Vector2)playerPos; // 그냥 벡터를 만듬
            dirVec = dirVec.normalized; // normalized를 꼭 해줘야 방향벡터

            GameObject tempObject = Instantiate(bulletObject, bulletContainer);
            
            // 총알의 오른쪽 방향을 dirVec로 설정
            tempObject.transform.right = dirVec;
            
            // 총알이 플레이어보다 살짝 앞에서 발사 
            tempObject.transform.position = (Vector2)playerPos + dirVec * 0.5f; 
            
            // 플레이어가 반대 방향으로 이동 (반동)
            // dirVec의 정반대의 벡터를 만들기 위해서 (-)를 곱해주면 됨.
            transform.Translate(-dirVec);
        }
    }
    
    void MouseCheck()
    {
        // 마우스의 위치값을 받음
        Vector2 mousePos = Input.mousePosition; 
        
        // 현재 마우스의 위치를 게임 내의 Position 값으로 변환
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);
        
        Vector3 playerPos = transform.position;
        
        Vector2 distanceVec = mousePos - (Vector2)playerPos;
        
        guideLine.SetActive(distanceVec.magnitude < ditectionRange ? true : false);
        // 가이드라인 활성화?
        // 일정 거리 안에 들어가면 활성화O
        // magnitude 이용하면 거리를 알 수 있음.
        // sqrMagnitude 거리의 제곱을 알 수 있음.
        
        guideLine.transform.right = distanceVec.normalized;
        // 가이드라인의 방향을 distanceVec의 방향벡터로 설정하겠다는 뜻.
        // 방향벡터를 설정하는 것은 벡터.normalized
        // distanceVec.normalized == distanceVec / distanceVec.magnitude
        // 방향에 관련된 것은 전부 방향벡터로 설정!
    }
}
