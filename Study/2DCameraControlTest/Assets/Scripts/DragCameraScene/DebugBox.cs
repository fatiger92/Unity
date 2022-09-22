using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBox : MonoBehaviour
{
    public GameObject[] Maps;
    
    void Awake()
    {
        
    }

    [ContextMenu("각 맵의 사이즈")]
    public void ShowMapSpriteSizes()
    {
        for (int i = 0; i < Maps.Length; i++)
        {
            var sprite = Maps[i].GetComponent<SpriteRenderer>().sprite;
            Debug.Log($"{i} 번째 맵 사이즈 :: {sprite.rect.size}");
        }
    }


    [ContextMenu("Sprite Bound")]
    public void ShowLocalMatrix()
    {
        for (int i = 0; i < Maps.Length; i++)
        {
            var sprite = Maps[i].GetComponent<SpriteRenderer>().sprite;
            Debug.Log($"{i} 번째 맵 Bounds :: {sprite.bounds}");
        }
    }
}
