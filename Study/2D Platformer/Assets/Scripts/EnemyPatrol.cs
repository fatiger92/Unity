using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    int currentPoint;

    public float moveSpeed;
    void Start()
    {
        foreach (var t in patrolPoints)
            t.SetParent(null);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
    }
}
