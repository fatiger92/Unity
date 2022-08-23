using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour
{
    private Rigidbody2D warriorRigidbody2D;

    public float jumpPower;
    public float speed;
    void Start()
    {
        warriorRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerMove();
        PlayerJump();
    }
    
    void PlayerMove()
    {
        float  x = Input.GetAxis("Horizontal"); // x축만 값을 받아서 사용
        transform.Translate(Vector3.right * (x * speed * Time.deltaTime));
        // Vector3.right(World) vs tranform.right(Player) 둘이 다름.
    }

    void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space)) // 스페이스키를 눌렀을 때
            warriorRigidbody2D.AddForce(Vector2.up * jumpPower,ForceMode2D.Impulse);
        // 힘을 위쪽 방향으로 jumpPower만큼 줄 것 인데 타입은 Impulse(순간적인 힘)
    }

    private void OnCollisionEnter2D(Collision2D _col)
    {
        // 벽의 아래쪽에? 위쪽에?
        // 부딪인 오브젝트의 태그가 Ground가 아닐 경우에만 발동
        if (_col.gameObject.tag != "Ground")
        {
            UpOrDown(_col);
        }
    }

    void UpOrDown(Collision2D _col)
    {
        Vector3 distVec = transform.position - _col.transform.position; 
        // Warrior Pos - Wall Pos
        // 벽 -> 전사 방향으로 벡터를 만든다.
        // 반시계 방향 - 엄지의 방향 (화면에서 밖으로 나옴) - 양수 : 벽의 위에 부딪힘
        // 시계 방향 - 엄지의 방향 (화면으로 들어감) - 음수 : 벽의 아래에 부딪힘
        if (Vector3.Cross(_col.transform.right, distVec).z > 0)
        {
            // _col.transform.right 충돌체의 오른쪽 방향 벡터
            Debug.Log("Up : 벽의 위에 부딪힘");
            return;
        }
        Debug.Log("Down : 벽의 아래에 부딪힘");
    }
 
}
