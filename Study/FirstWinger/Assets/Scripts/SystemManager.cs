using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    private static SystemManager instance;
    public static SystemManager Instance => instance;
    
    [SerializeField] private Player player;
    public Player Hero => player;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("SystemManager error!! Singleton error");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }


}
