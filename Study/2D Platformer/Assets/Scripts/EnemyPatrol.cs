using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    int currentPoint;

    public float moveSpeed;
    public float timeAtPoints;
    float waitCounter;
    
    void Start()
    {
        foreach (var t in patrolPoints)
            t.SetParent(null);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < .001f) // 엄청 가깝다면
        {
            currentPoint++;

            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
