using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    void Awake()
    {
        instance = this;
    }

    public int currentHealth, maxHealth;

    // invincibility 무적
    public float invincibilityLength = 1f;
    float invincibilityCounter;

    public SpriteRenderer theSR;
    public Color normalColor, fadeColor;

    PlayerController thePlayer;
    
    void Start()
    {
        thePlayer = GetComponent<PlayerController>();
        
        currentHealth = maxHealth;
        
        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }

    void Update()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            if (invincibilityCounter <= 0)
                theSR.color = normalColor;
        }
    }

    public void DamagePlayer()
    {
        if (invincibilityCounter > 0) 
            return;
        
        currentHealth--;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);
        }
        else
        {
            invincibilityCounter = invincibilityLength;
            theSR.color = fadeColor;
            thePlayer.KnockBack();
        }
        
        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }
}
