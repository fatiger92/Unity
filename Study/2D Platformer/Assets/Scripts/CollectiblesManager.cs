using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager instance;

    void Awake()
    {
        instance = this;
    }

    public int collectibleCount;
    public int extraLifeThreshold; // Threshold : 한계점
    
    void Start()
    {
        if (UIController.instance != null)
        {
            UIController.instance.UpdateCollectibles(collectibleCount);
        }
    }
    
    void Update()
    {
        
    }

    public void GetCollectible(int amount)
    {
        collectibleCount += amount;

        if (collectibleCount >= extraLifeThreshold)
        {
            collectibleCount -= extraLifeThreshold;

            if (LifeController.instance != null)
            {
                LifeController.instance.AddLife();
            }
        }

        if (UIController.instance != null)
        {
            UIController.instance.UpdateCollectibles(collectibleCount);
        }
    }
}
