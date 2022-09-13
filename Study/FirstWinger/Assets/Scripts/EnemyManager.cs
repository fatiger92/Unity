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

    public bool GenerateEnemy(SquadronMemberStruct data)
    {
        //GameObject go = SystemManager.Instance.EnemyCacheSystem.Archive(filePath);
        string FilePath = SystemManager.Instance.EnemyTable.GetEnemy(data.EnemyID).FilePath;
        GameObject go = SystemManager.Instance.EnemyCacheSystem.Archive(FilePath);

        go.transform.position = new Vector3(data.GeneratePointX, data.GeneratePointY, 0);
        
        Enemy enemy = go.GetComponent<Enemy>();
        
        //enemy.FilePath = data.FilePath;
        enemy.FilePath = FilePath;
        
        enemy.Reset(data);

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
