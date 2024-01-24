using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleActivator : MonoBehaviour
{
    public BossBattleController theBoss;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            theBoss.ActivateBattle();
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
