using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelsMap : MonoBehaviour
{
    public GameObject[] Maps;

    public MapCamera mapCamera;
    
    [ContextMenu("맵 해금")]
    public void ActiveMaps()
    {
        if (Maps[Maps.Length - 1].activeSelf)
        {
            Debug.Log("전부 해금되었습니다.");
            return;
        }
        
        for (var i = 0; i < Maps.Length; i++)
        {
            if (Maps[i].activeSelf) 
                continue;
            
            Maps[i].SetActive(true);
            break;
        }
        
        mapCamera.MapSizeCheck();
    }
}
