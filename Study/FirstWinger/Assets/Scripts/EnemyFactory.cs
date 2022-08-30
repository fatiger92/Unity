using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
   public const string EnemyPath = "Prefabs/Enemy";

   private Dictionary<string, GameObject> EnemyFileCache = new Dictionary<string, GameObject>();

   public GameObject Load(string resourcePath)
   {
      GameObject go = null;
      
      if (EnemyFileCache.ContainsKey(resourcePath))
      {
         go = EnemyFileCache[resourcePath];
      }
      else
      {
         go = Resources.Load<GameObject>(resourcePath);
         if (!go)
         {
            Debug.LogError($"Load error ! path = {resourcePath}");
            return null;
         }
         EnemyFileCache.Add(resourcePath, go);
      }
      GameObject InstancedGO = Instantiate<GameObject>(go);
      return InstancedGO;
   }
}
