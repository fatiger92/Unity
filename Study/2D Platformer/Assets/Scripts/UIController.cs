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
    public GameObject pauseScreen;
    
    public string MainMenuScene;
    public Image fadeScreen;
    public float fadeSpeed;
    public bool fadingToBlack, fadingFromBlack;
    
    void Start()
    {
        FadeFromBlack();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
#endif

        if (fadingFromBlack)
        {
            fadeScreen.color = new Color(
                fadeScreen.color.r,
                fadeScreen.color.g,
                fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        }

        if (fadingToBlack)
        {
            fadeScreen.color = new Color(
                fadeScreen.color.r,
                fadeScreen.color.g,
                fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
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
        Time.timeScale = 1f;
    }

    public void UpdateCollectibles(int amount)
    {
        collectibleText.text = $"x{amount}";
    }

    void PauseUnpause()
    {
        bool isPaused = pauseScreen.activeSelf;
        
        pauseScreen.SetActive(isPaused == false);
        Time.timeScale = isPaused == false ? 0f : 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("I Quit");
    }

    public void FadeFromBlack()
    {
        fadingToBlack = false;
        fadingFromBlack = true;
    }

    public void FadeToBlack()
    {
        fadingToBlack = true;
        fadingFromBlack = false;
    }
}
