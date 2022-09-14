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
   
    [SerializeField] private EnemyTable enemyTable;
    public EnemyTable EnemyTable => enemyTable;

    private BaseSceneMain currentSceneMain;
    
    public BaseSceneMain CurrentSceneMain { set { currentSceneMain = value; } }

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

    private void Start()
    {
        BaseSceneMain baseSceneMain = GameObject.FindObjectOfType<BaseSceneMain>();
        Debug.Log("OnSceneLoaded ! baseSceneMain.name = " + baseSceneMain.name);
        Instance.CurrentSceneMain = baseSceneMain;
    }

    public T GetCurrentSceneMain<T>() where T : BaseSceneMain
    {
        return currentSceneMain as T;
    }
}
