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

    public Animator anim;
    public EnemyController theEC;

    public bool shouldChasePlayer;
    bool isChasing;
    public float distanceToChasePlayer;
    PlayerController thePlayer;
    
    void Start()
    {
        foreach (var t in patrolPoints)
            t.SetParent(null);

        waitCounter = timeAtPoints;

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        theEC = GetComponent<EnemyController>();
        
        anim.SetBool(ANIM_PARAM_ISMOVING, true);

        if (shouldChasePlayer == true)
        {
            thePlayer = FindFirstObjectByType<PlayerController>();
        }
    }

    void Update()
    {
        if (theEC.isDefeated != false) 
            return;
        
        if (shouldChasePlayer == true)
        {
            if (isChasing == false)
            {
                if (Vector3.Distance(transform.position, thePlayer.transform.position) < distanceToChasePlayer)
                {
                    isChasing = true;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, thePlayer.transform.position) > distanceToChasePlayer)
                {
                    var direction = transform.position.x < patrolPoints[currentPoint].position.x ? -1f : 1f;
                    transform.localScale = new Vector3(direction, 1f, 1f);
                    
                    isChasing = false;
                }
            }
        }

        if (shouldChasePlayer == false || (shouldChasePlayer == true && isChasing == false))
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position,
                moveSpeed * Time.deltaTime);

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
        else if(isChasing == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position,
                moveSpeed * Time.deltaTime);
            
            transform.localScale = transform.position.x > thePlayer.transform.position.x ? Vector3.one : new Vector3(-1f, 1f, 1f);
        }
    }
}
