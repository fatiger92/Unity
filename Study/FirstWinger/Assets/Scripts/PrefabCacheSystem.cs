using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefabCacheData
{
    public string filePath;
    public int cacheCount;
}

public class PrefabCacheSystem
{
    private Dictionary<string, Queue<GameObject>> Caches = new Dictionary<string, Queue<GameObject>>();

    public void GenerateCache(string filePath, GameObject gameObject, int cacheCount, Transform parentTransform = null)
    {
        if (Caches.ContainsKey(filePath))
        {
            Debug.LogWarning($"Already cache generated! filePath = {filePath}");
            return;
        }

        var queue = new Queue<GameObject>();
        for (int i = 0; i < cacheCount; i++)
        {
            var go = Object.Instantiate<GameObject>(gameObject, parentTransform);
            go.SetActive(false);
            queue.Enqueue(go);
        }
        Caches.Add(filePath, queue);
    }

    public GameObject Archive(string filePath)
    {
        if (!Caches.ContainsKey(filePath))
        {
            Debug.LogError($"Archive Error! no Cache generated! filePath = {filePath}");
            return null;
        }

        if (Caches[filePath].Count == 0)
        {
            Debug.LogWarning("Archive problem! not enough count");
            return null;
        }

        var go = Caches[filePath].Dequeue();
        go.SetActive(true);

        return go;
    }

    public bool Restore(string filePath, GameObject gameObject)
    {
        if (!Caches.ContainsKey(filePath))
        {
            Debug.LogError($"Restore Error! no Cache generated! filePath = {filePath}");
            return false;
        }
        
        gameObject.SetActive(false);
        
        Caches[filePath].Enqueue(gameObject);
        return true;
    }
}
