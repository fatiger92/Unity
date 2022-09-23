using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugBox : MonoBehaviour
{
    public Mine[] Maps;
    public Button btn_createMap;

    public LevelsMap LevelsMap;
    
    public void Initialize()
    {
        btn_createMap.onClick.AddListener(() => LevelsMap.OpenMap());   
    }

    void Start()
    {
        Initialize();
        Maps = LevelsMap.Maps;
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
            Debug.Log($"{i} 번째 확장 맵 Bounds :: {Maps[i].extendMapSr.bounds}");
        }
    }
}
