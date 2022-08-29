using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 행렬을 직접 게임에서 적용을 하는 경우는
// DirectX, OpenGL 공부를 하게 되면 많이 사용
// shader, 좀 더 심화된 내용을 공부를 할 때 이 내용이 더 유용하게 사용이 된다.
// 내부적으로 transform 값이 행렬을 이용해서 연산이 된다는 것을 보여주고 싶었음.
public class Matrix : MonoBehaviour
{
    private Matrix4x4 worldMat;
    void Start()
    {
        MakeWorldMatrix();
        ExtractionMatrix();
    }

    // Quaternion rot = Quaternion.Euler(45, 0, 45); // 쿼터니언
    // 회전행렬을 이용한다고 했음. ( 오일러 각 - 직관적으로 각도를 이용한 것임 )
    // 오일러 각을 이용해서 회전행렬을 적용하게 되면 짐벌락이라는 현상이 일어남
    // 짐벌락 : 각 회전에 대해서 서로가 종속적이어서 생기는 문제
    // 짐벌락 문제 => 쿼터니언(사원수)
    //transform.rotation = new Vector3(1f, 1f, 1f); (X) // 오일러 각으로 대입
    // 유니티는 내부적으로 쿼터니언을 사용해서 연산
    // 오일러 각 -> 쿼터니언으로 변환해서 사용
    // 회전에 관한 것은 전부 ***쿼터니언***을 걸쳐서 연산을 해야함.
    // 쿼터니언 -> 변환행렬
    
    void MakeWorldMatrix()
    {
        Vector3 tran = new Vector3(2, 1, 0);
        Quaternion rot = Quaternion.Euler(45, 0, 45); // 쿼터니언

        Vector3 scal = new Vector3(3, 3, 3);

        //worldMat = Matrix4x4.TRS(tran, rot, scal);
        worldMat = Matrix4x4.Translate(new Vector3(2, 1, 0)) * Matrix4x4.Rotate(rot) * Matrix4x4.Scale(new Vector3(3,3,3));
        // 이동행렬, 회전행렬, 크기변환행렬
        
        Debug.Log("=== Make Matrix ===");
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(worldMat.GetRow(i));
        }
    }
    private void ExtractionMatrix()
    {
        Matrix4x4 matrix = transform.localToWorldMatrix;
        // localToWorldMatrix를 저장하겠다.
        // localToWorldMatrix를 :: TRS의 결과 변환 행렬 값과 동일하다.
        Debug.Log("=== ExtractionMatrix ===");
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(matrix.GetRow(i));
        }
        // Row 행, Column 열
        
        
        // ====================================
        // 변환행렬 -> position, rotation, scale
        
        Vector3 position = matrix.GetColumn(3);
        Debug.Log("=== Position ===");
        Debug.Log(position);
        
        Quaternion rotation = Quaternion.LookRotation(
            matrix.GetColumn(2),
            matrix.GetColumn(1)
        );
        
        Debug.Log("=== Rotation ===");
        Debug.Log(rotation.eulerAngles);
        
        Debug.Log("=== Scale ===");
        Vector3 scale = new Vector3(
            matrix.GetColumn(0).magnitude,
            matrix.GetColumn(1).magnitude,
            matrix.GetColumn(2).magnitude
        );
        Debug.Log(scale);
    }
}
