using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    PlayerHealthController _healthController;
    
    void Start()
    {
        _healthController = FindFirstObjectByType<PlayerHealthController>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);
            //FindFirstObjectByType<PlayerHealthController>().DamagePlayer();
            
            _healthController.DamagePlayer();
        }
    }
}
