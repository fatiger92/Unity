using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    static readonly int ANIM_PARAM_ISMOVING = Animator.StringToHash("isMoving");
    
    public Transform[] patrolPoints;
    int currentPoint;

    public float moveSpeed;
    public float timeAtPoints;
    float waitCounter;

    Animator anim;
    public EnemyController theEC;
    
    void Start()
    {
        foreach (var t in patrolPoints)
            t.SetParent(null);

        waitCounter = timeAtPoints;

        anim = GetComponent<Animator>();
        theEC = GetComponent<EnemyController>();
        
        anim.SetBool(ANIM_PARAM_ISMOVING, true);
    }

    void Update()
    {
        if (theEC.isDefeated == true)
            return;
        
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < .001f) // 엄청 가깝다면
        {
            waitCounter -= Time.deltaTime;
            anim.SetBool(ANIM_PARAM_ISMOVING, false);
            
            if (waitCounter <= 0)
            {
                currentPoint++;

                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }

                waitCounter = timeAtPoints;
                anim.SetBool(ANIM_PARAM_ISMOVING, true);

                var direction = transform.position.x < patrolPoints[currentPoint].position.x ? -1f : 1f;
                transform.localScale = new Vector3(direction, 1f, 1f);
            }
        }
    }
}
