using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int currentHealth, maxHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void DamagePlayer()
    {
        currentHealth--;
    }
}
