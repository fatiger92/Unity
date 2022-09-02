using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;

    private List<Enemy> enemies = new List<Enemy>();
    public List<Enemy> Enemies => enemies;
    [SerializeField] private PrefabCacheData[] enemyFiles;

    private void Start()
    {
        Prepare();
    }

    void Update()
    {
        
    }

    public bool GenerateEnemy(EnemyGenerateData data)
    {
        //GameObject go = SystemManager.Instance.EnemyCacheSystem.Archive(filePath);
        GameObject go = SystemManager.Instance.EnemyCacheSystem.Archive(data.FilePath);
        
        go.transform.position = data.GeneratePoint;

        Enemy enemy = go.GetComponent<Enemy>();
        
        //enemy.FilePath = filePath;
        enemy.FilePath = data.FilePath;
        //enemy.Appear(new Vector3(7.0f, 0.0f, 0.0f));
        //enemy.Reset(data);
        
        enemies.Add(enemy);
        return true;
    }

    public bool RemoveEnemy(Enemy enemy)
    {
        if (!enemies.Contains(enemy))
        {
            Debug.LogError("No exist Enemy");
            return false;
        }

        enemies.Remove(enemy);
        SystemManager.Instance.EnemyCacheSystem.Restore(enemy.FilePath, enemy.gameObject);

        return true;
    }

    public void Prepare()
    {
        for (int i = 0; i < enemyFiles.Length; i++)
        {
            var go = enemyFactory.Load(enemyFiles[i].filePath);
            SystemManager.Instance.EnemyCacheSystem.GenerateCache(enemyFiles[i].filePath, go, enemyFiles[i].cacheCount);
        }
    }
}
