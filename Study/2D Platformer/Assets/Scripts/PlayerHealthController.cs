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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddHealth(1);
        }
#endif
    }

    public void DamagePlayer()
    {
        if (invincibilityCounter > 0) 
            return;
        
        currentHealth--;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //gameObject.SetActive(false);
            
            // 플레이어가 죽었을 경우
            LifeController.instance.Respawn();
            
            AudioManager.instance.PlaySFX(11);
        }
        else
        {
            invincibilityCounter = invincibilityLength;
            theSR.color = fadeColor;
            thePlayer.KnockBack();
            AudioManager.instance.PlaySFX(13);
        }
        
        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }

    public void AddHealth(int amountToAdd)
    {
        currentHealth += amountToAdd;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }
}
