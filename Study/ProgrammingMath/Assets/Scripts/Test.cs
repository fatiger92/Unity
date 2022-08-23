using UnityEngine;

public class Test : MonoBehaviour
{
    [ContextMenu("벡터의 외적")]
    public void CalculateOuterProductVec()
    {
        var _vecOne = new Vector3(2f, 4f, 3f);
        var _vecTwo = new Vector3(5f, 1f, 2f);
        
        // x = y1z2 - z1y2 = 4*2 - 3*1 = 5
        // y = z1x2 - x1z2 = 3*5 - 2*2 = 11
        // z = x1y2 - y1x2 = 2*1 - 4*5 = -18

        var _outerProductVec = Vector3.Cross(_vecOne, _vecTwo);
        Debug.Log($"x 값 : {_outerProductVec.x}, y 값 : {_outerProductVec.y}, z 값 {_outerProductVec.z}");
        
        // 출력 값 :: x 값 : 5, y 값 : 11, z 값 -18
        
        // 강의에서 알려준 공식의 경우
        // x = y1z2 - z1y2 = 4*2 - 3*1 = 5 
        // y = x1z2 - z1x2 = 2*2 - 3*5 = -11
        // z = x1y2 - x2y1 = 2*1 - 5*4 = -18
        
        // 예상 출력 값 :: x 값 : 5, y 값 : -11, z 값 -18
    }
}
