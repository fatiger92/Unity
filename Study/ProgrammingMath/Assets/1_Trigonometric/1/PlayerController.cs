using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 4가지의 실습.
// 1. 플레이어를 원하는 방향으로 이동
// 2. 플레이어가 원을 그리면서 이동 ( 싸이클로이드 곡선, 꽃모양 패턴 )
// 3. 미사일을 발사 (순차적으로)
// 4. 미사일을 한번에 발사 (특정 범위)
public enum Pattern { One, Two };
public class PlayerController : MonoBehaviour
{
    public GameObject bulletObject;
    public Transform bulletContainer;

    public Pattern shotPattern; // 패턴을 선택할 수 있게 되어 있음
    public float moveSpeed = 10f;
    public float circleScale = 5f;
    public int angleInterval = 10;
    public int startAngle = 30;
    public int endAngle = 330;

    private int iteration = 0;
    
    private void Start()
    {
        if(shotPattern == Pattern.One)
            StartCoroutine(MakeBullet());
        else if (shotPattern == Pattern.Two)
            StartCoroutine(MakeBullet2());
    }

    private void Update()
    {
        //PlayerMove(30);
        // 360 deg를 넘은  450 deg 경우 360 + 90 deg 이므로 90 deg로 이동함.
        //PlayerCircle();
    }

    void PlayerMove(float _angle)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Vector2 direction = new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad));
            // Mathf.cos,sin은 기본적으로 Rad 값을 기준으로 적용된다.
            // 따라서 Deg 값을 사용하기 위해서는 파라미터로 받아온 Deg 값 _angle에다가 Mathf.Deg2Rad를 곱해서 라디안 값으로 만들어줘야 하는 것이다.
            // Rad 0~2pi Deg 0~360, Deg값을 Rad값으로 변경
            transform.Translate(moveSpeed * direction * Time.deltaTime);
            // Time.deltaTime 프레임 간의 시간 차이를 곱해줘야 컴퓨터의 성능과 무관하게 동일한 속도로 이동 가능.
        }
    }

    void PlayerCircle()
    {
        // iteration 0 -> 360 각도가 1씩 증가
        // Deg, Deg2Rad로 변환
        // circleScale 원의 크기를 결정
        
        Vector2 direction = new Vector2(Mathf.Cos(iteration * Mathf.Deg2Rad), Mathf.Sin(iteration * Mathf.Deg2Rad));
        transform.Translate(direction * (circleScale * Time.deltaTime));
        iteration++;
        if (iteration > 360) iteration -= 360;
    }

    private IEnumerator MakeBullet()
    {
        int fireAngle = 0;
        while (true)
        {
            GameObject tempObject = Instantiate(bulletObject, bulletContainer, true);
            // bulletContainer 안에 bulletObject을 생성하겠다.
            Vector2 direction = new Vector2(Mathf.Cos(fireAngle*Mathf.Deg2Rad),Mathf.Sin(fireAngle*Mathf.Deg2Rad));
            tempObject.transform.right = direction;
            // 총알 오브젝트 오른쪽을 direction 방향으로 설정
            tempObject.transform.position = transform.position;
            // 총알 오브젝트의 위치는 플레이어의 위치로 설정
            
            yield return new WaitForSeconds(0.1f);
            // 0.1초간 기다리고
            
            fireAngle += angleInterval;
            // 발사한 각도를 설정한 값에 따라서 증가해준다.
            if (fireAngle > 360) fireAngle -= 360;
        }
    }
    
    private IEnumerator MakeBullet2()
    {
        while (true)
        {
            // 한번에 미사일을 만들어 줌
            for (int fireAngle = startAngle; fireAngle < endAngle; fireAngle += angleInterval)
            {
                GameObject tempObject = Instantiate(bulletObject, bulletContainer, true);
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle*Mathf.Deg2Rad),Mathf.Sin(fireAngle*Mathf.Deg2Rad));
               
                tempObject.transform.right = direction;
                tempObject.transform.position = transform.position;
            }

            yield return new WaitForSeconds(4f);
            // 4초간 대기
        }
    }
}
