using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    void Awake()
    {
        instance = this;
    }

    public Image[] healthIcons;
    public Sprite heartFull, heartEmpty;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateHealthDisplay(int health, int maxHealth)
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            healthIcons[i].enabled = true;

            /* if (health <= i)
            {
                healthIcons[i].enabled = false;
            }*/

            healthIcons[i].sprite = health > i ? heartFull : heartEmpty;
            
            if (maxHealth <= i) // 최대체력 넘어서 출력되는 빈 하트를 지우기 위해
                healthIcons[i].enabled = false;
        }
    }
}
