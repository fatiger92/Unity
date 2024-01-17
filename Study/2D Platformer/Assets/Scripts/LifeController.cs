using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public static LifeController instance;
    PlayerController thePlayer;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        thePlayer = FindFirstObjectByType<PlayerController>();
        
        // 아래가 더 자원소모 적음 - 내가 생각한거
        //thePlayer = PlayerHealthController.instance.gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }

    public void Respawn()
    {
        // 리스폰은 적게 일어나기 때문에 Find를 써도 된다는 강사의 주장
        thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;
        PlayerHealthController.instance.AddHealth(PlayerHealthController.instance.maxHealth);
    }
}
