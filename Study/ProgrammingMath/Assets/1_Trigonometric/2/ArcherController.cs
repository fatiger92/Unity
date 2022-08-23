using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    public GameObject arrowObject;
    public Transform arrowContainer;

    public float shotInterval = 2f;
    
    void Start()
    {
        StartCoroutine(FireArrow());
    }

    IEnumerator FireArrow()
    {
        while (true)
        {
            // 20 + 20 * i
            // 20 40 60
            for (int i = 0; i < 3; i++)
            {
                GameObject tempObject = Instantiate(arrowObject, arrowContainer);
                Vector3 direction = new Vector2(Mathf.Cos((20+20*i)*Mathf.Deg2Rad), Mathf.Sin((20+20*i)*Mathf.Deg2Rad));

                tempObject.transform.right = direction;
                tempObject.transform.position = transform.position + shotInterval * direction;
            }

            yield return new WaitForSeconds(5f);
        }
    }

}
