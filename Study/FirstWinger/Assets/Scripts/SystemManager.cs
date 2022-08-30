using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    /// <summary>
    /// 싱글톤 인스턴스
    /// </summary>
    private static SystemManager instance;
    public static SystemManager Instance => instance;
    
    [SerializeField] private Player player;
    
    public Player Hero
    {
        get
        {
            if (!player)
            {
                Debug.LogError("Main Player is not setted!");
            }
            return player;
        }
    }

    private GamePointAccumulator gamePointAccumulator = new GamePointAccumulator();
    public GamePointAccumulator GamePointAccumulator => gamePointAccumulator;

    [SerializeField] private EffectManager effectManager;
    public EffectManager EffectManager => effectManager;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("SystemManager is initialized twice!");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
