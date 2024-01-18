using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.currentHealth = 0;
            UIController.instance.UpdateHealthDisplay(PlayerHealthController.instance.currentHealth, PlayerHealthController.instance.maxHealth);
            LifeController.instance.Respawn();
        }
    }
}
