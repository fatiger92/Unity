using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceController : MonoBehaviour
{
    private Rigidbody boxRigidbody;
    private float movePower = 5f;
    void Start()
    {
        boxRigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        // Impulse는 1초 동안 가해진 힘의 합
        // FixedUpdate 1번의 step이 0.02s, 1초는 50번 (default)
        // ProjectSetting/Time에서 Fixed Timestep 값을 조절할 수 있음.
        // 힘을 50번, Force * 50
        // Force / Fixed Timestep
        
        Debug.Log(boxRigidbody.velocity);
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            boxRigidbody.AddForce(transform.right * movePower, ForceMode.Impulse);
            //boxRigidbody.AddForce(transform.right * movePower / Time.fixedDeltaTime, ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.S))
            boxRigidbody.AddForce(transform.right * movePower, ForceMode.Force);
        // Impulse와 Force는 전부 질량에 영향을 받습니다. F = ma
        // Impulse 게임에서는 순간적인 힘을 가할 때
        // Force 게임에서는 지속적으로 힘을 가할 때

        else if (Input.GetKeyDown(KeyCode.D))
        {
            boxRigidbody.AddForce(transform.right * movePower, ForceMode.VelocityChange);
            //boxRigidbody.AddForce(transform.right * movePower / Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.F))
            boxRigidbody.AddForce(transform.right * movePower, ForceMode.Acceleration);
        // VelocityChange, Acceleration는 질량과 무관하게 힘을 가합니다.
        // VelocityChange 게임에서는 순간적인 힘을 가할 때
        // Acceleration는 게임에서는 지속적으로 힘을 가할 때
    }
}
