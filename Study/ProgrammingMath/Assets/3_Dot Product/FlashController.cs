using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class FlashController : MonoBehaviour
{
    public GameObject[] ghostObjectArray;

    public float moveSpeed = 3f;
    public float rangeAngle = 25f;
    public float rangeDistance = 4f;
    void Update()
    {
        PlayerMove();
        CheckGhost();
    }

    void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        // GetAxis 조이스틱 컨트롤러를 만들 수 있습니다.
        transform.Translate(new Vector3(x,y) * (moveSpeed * Time.deltaTime));
    }

    void CheckGhost()
    {
        int i = 0; // 유령의 수 0
        
        // foreach를 이용해서 각 오브젝트를 전부 체크
        foreach (var ghost in ghostObjectArray)
        {
            Vector3 distanceVec = ghost.transform.position - transform.position;
            
            if (distanceVec.magnitude < rangeDistance)
            {
                // 내적을 하기 위해서 무조건 방향 벡터로 만들어 줌
                Vector3 dirVec = distanceVec.normalized; 
                if(Vector3.Dot(transform.up, dirVec) > Mathf.Cos(rangeAngle*Mathf.Deg2Rad))
                    i++;
                // Vector3.Dot 하면 내적해줌.
                // 결과적으로 둘 다 방향벡터
                // x1y1 + x2y2 (X Vector3.Dot
            }
        }
        
        Debug.Log("감지된 유령의 수: "+i);
    }
}
