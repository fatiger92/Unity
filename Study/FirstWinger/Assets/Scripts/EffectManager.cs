using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject[] effectPrefabs;

    public GameObject GenerateEffect(int index, Vector3 position)
    {
        if (index < 0 || index >= effectPrefabs.Length)
        {
            Debug.LogError($"GenerateEffect error! out of range! index = {index}");
            return null;
        }

        GameObject go = Instantiate<GameObject>(effectPrefabs[index], position, Quaternion.identity);
        return go;
    }
}
