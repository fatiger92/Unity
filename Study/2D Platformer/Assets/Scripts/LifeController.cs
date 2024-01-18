using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public static LifeController instance;
    PlayerController thePlayer;

    // 2초가 딱 좋다고 하는데 나는 1.2초가 좋은 것 같음, 2초 너무 길어버림 죽은 것도 빡치는데 기다려야 돼서 더 빡침
    public float respawnDelay = 1.2f;
    public int currentLives = 3;

    public GameObject deathEffect, respawnEffect;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        thePlayer = FindFirstObjectByType<PlayerController>();
        
        // 아래가 더 자원소모 적음 - 내가 생각한거
        //thePlayer = PlayerHealthController.instance.gameObject.GetComponent<PlayerController>();
        
        if (UIController.instance != null)
        {
            UIController.instance.UpdateLivesDisplay(currentLives);
        }
    }

    void Update()
    {
        
    }

    public void Respawn()
    {
        // 리스폰은 적게 일어나기 때문에 Find를 써도 된다는 강사의 주장
        //thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;
        //PlayerHealthController.instance.AddHealth(PlayerHealthController.instance.maxHealth);
        
        thePlayer.gameObject.SetActive(false);
        thePlayer.theRB.velocity = Vector2.zero;

        currentLives--;

        if (currentLives > 0)
        {
            StartCoroutine(RespawnCo());
        }
        else
        {
            currentLives = 0;
            StartCoroutine(GameOverCo());
        }

        if (UIController.instance != null)
        {
            UIController.instance.UpdateLivesDisplay(currentLives);
        }

        Instantiate(deathEffect, thePlayer.transform.position, deathEffect.transform.rotation);
    }
    
    IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(respawnDelay);
        
        thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;
        PlayerHealthController.instance.AddHealth(PlayerHealthController.instance.maxHealth);
        
        thePlayer.gameObject.SetActive(true);
        
        Instantiate(respawnEffect, thePlayer.transform.position, respawnEffect.transform.rotation);
    }

    IEnumerator GameOverCo()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (UIController.instance != null)
        {
            UIController.instance.ShowGameOver();
        }
    }
}
