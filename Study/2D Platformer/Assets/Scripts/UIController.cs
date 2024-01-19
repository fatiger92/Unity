using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    void Awake()
    {
        instance = this;
    }

    public Image[] healthIcons;
    public Sprite heartFull, heartEmpty;

    public TMP_Text livesText, collectibleText;
    public GameObject gameOverScreen;
    
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

    public void UpdateLivesDisplay(int currentLives)
    {
        livesText.text = $"x{currentLives}";
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        //Debug.Log("Restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateCollectibles(int amount)
    {
        collectibleText.text = $"x{amount}";
    }
}
