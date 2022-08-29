using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float shotVelocity;
    public float shotAngle;

    private Rigidbody2D ballRB2D;
    private bool isGround = true;
    private bool isCenter = false;
    private float totalTime = 0f;

    void Start()
    {
        ballRB2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShotBall());
        }
    }
    IEnumerator ShotBall()
    {
        Debug.Log("=== Simulation ===");

        isGround = false;
        
        // 공의 각도 설정
        transform.right = new Vector2(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));
        
        // 설정된 각도로 shotVelocity 속도로 발사를 함.
        ballRB2D.velocity = transform.right * shotVelocity;

        // 처음 시간은 0으로 초기화
        totalTime = 0f;
        
        while (true)
        {
            yield return null;
            if (isGround) break; // 착지를 하면 while문이 끝남.
            totalTime += Time.deltaTime; // 착지를 하기 전까지 시간을 계속 측정
            
            // y축의 속도의 절댓값이 0.1보다 작을 때 && isCenter가 false일 때 (딱 1번만 발동)
            if (Mathf.Abs(ballRB2D.velocity.y) < 0.1f && !isCenter)
            {
                isCenter = true;
                Debug.Log("CenterHeight: " + transform.position.y); // 최고 높이
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D _col)
    {
        if(isGround == false) // 그라운드에 착지
        {
            isGround = true;
            ballRB2D.velocity = Vector2.zero; // 속도는 0 더 이상 움직이기 않게 하기 위해)
            Debug.Log("Totaltime: " + totalTime); // 총 걸린 시간
            Debug.Log("TotalMeter: " + (transform.position.x + 8)); // 초기 위치가 -8에서 시작을 했기 때문에 / +8로 보정

            Verification();
        }
    }

    private void Verification()
    {
        Debug.Log("=== Verification ===");

        float totalTime = 2 * shotVelocity * Mathf.Sin(shotAngle * Mathf.Deg2Rad) / 9.81f; 
        // 총 걸린 시간은 2t
        // 2*V*sin(theta)/g
        float centerHeight = Mathf.Pow(shotVelocity * Mathf.Sin(shotAngle * Mathf.Deg2Rad), 2) / (2*9.81f); 
        // 최고 높이
        // (V*sin(theta))^2 / 2g
        float totalMeter = Mathf.Pow(shotVelocity,2) / 9.81f * Mathf.Sin(2 * shotAngle * Mathf.Deg2Rad); 
        // 총 날아간 거리
        // 2*V^2*sin(theta)*cos(theta)/g => 2sin(theta)cos(theta) == sin(2theta)
        // v^2/g*sin(2*theta)

        Debug.Log("Totaltime: " + totalTime);
        Debug.Log("CenterHeight: " + centerHeight);
        Debug.Log("TotalMeter: " + totalMeter);
        
        // 위의 값과 메서드를 써서 나오는 값이 완전히 일치하지 않는 이유는
        // ProjectSettings의 Physics 2D 값이 조금 다르기 때문.
    }
}
