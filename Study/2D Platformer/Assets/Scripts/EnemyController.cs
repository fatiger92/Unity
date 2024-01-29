using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    static readonly int ANIM_PARAM_DEFEATED = Animator.StringToHash("defeated");

    [HideInInspector]
    public bool isDefeated;

    public float waitToDestory;
    
    public Animator anim;
        
    
    void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (isDefeated == true)
        {
            waitToDestory -= Time.deltaTime;

            if (waitToDestory <= 0)
            {
                AudioManager.instance.PlaySFX(5);
                
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(isDefeated == false)
                PlayerHealthController.instance.DamagePlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Destroy(gameObject);
            
            FindFirstObjectByType<PlayerController>().Jump();
            anim.SetTrigger(ANIM_PARAM_DEFEATED);

            isDefeated = true;
            
            AudioManager.instance.PlaySFX(6);
        }
    }
}
