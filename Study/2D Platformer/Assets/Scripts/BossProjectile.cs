using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 8f;
    Vector3 direction;

    public float lifetime = 3f;
    //int groundLayerIndex;
    void Start()
    {
        //groundLayerIndex = LayerMask.NameToLayer("Ground");
        
        direction = (PlayerHealthController.instance.transform.position - transform.position).normalized;
        
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += direction * Time.deltaTime * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer();
            Destroy(gameObject);
        }
        
        // if (other.gameObject.layer == groundLayerIndex)
        // {
        //     Destroy(gameObject);
        // }
    }
}
