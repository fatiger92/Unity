using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelsMap : MonoBehaviour
{
    //public static readonly float MAP_SPRITE_BOUNDX = 7.68f;
    //public static readonly float MAP_SPRITE_BOUNDY = 10.24f;
    
    public Mine[] Maps;
    public MapCamera mapCamera;
    public GameObject mapsPf;
    
    public static readonly int MAP_COUNT = 8;
    public static readonly int START_MAP_INDEX = 0;
    
    public int mapIdx = 0;
    public bool isLoaded = false;
    
    void Start()
    {
        CreateMaps();
        LoadMaps();
        ActiveMaps();
    }

    public void CreateMaps()
    {
        Maps = new Mine[MAP_COUNT];

        for (var i = 0; i < Maps.Length; i++)
        {
            var go = Instantiate(mapsPf, transform);
            var script = go.GetComponent<Mine>();
            script.OnNextStage(OpenMap, true);
            
            go.transform.position = new Vector2(0f, -script.MapBoundY * 2 * i);
            go.transform.rotation = Quaternion.identity;
            
            if (i == Maps.Length - 1)
                script.extendMap.SetActive(false);
            
            Maps[i] = script;
        }
    }

    public void LoadMaps() // 맵 데이터를 로드해온다.
    {
    }

    public void ActiveMaps() // 로드해 온 데이터로 맵을 세팅 한다.
    {

        for (var i = 0; i < Maps.Length; i++) // 뭐가 있던지 다 끈다.
            Maps[i].gameObject.SetActive(false);
        
        if (!isLoaded) // 로드된 데이터가 없으면 이쪽 분기 - 기본 세팅
        {
            Maps[START_MAP_INDEX].gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < mapIdx; i++)
            {
                Maps[i].gameObject.SetActive(true);

                if (i > 0)
                {
                    Maps[i - 1].OnNextStage(OpenMap, false);
                    Maps[i - 1].extendMap.SetActive(false);
                }
            }
        }
        
        mapCamera.MapSizeCheck(Maps);
    }

    public void OpenMap() // 맵을 추가 버튼 늘렀을 때 호출 되는 메서드
    {
        // 맵의 마지막 요소가 활성화 되어 있을 경우 탈출
        if (Maps[Maps.Length - 1].gameObject.activeSelf) 
        {
            Debug.Log("전부 해금되었습니다.");
            return;
        }
        
        // 순서대로 해금
        for (var i = 0; i < Maps.Length; i++)
        {
            if (Maps[i].gameObject.activeSelf) 
                continue;
            
            Maps[i].gameObject.SetActive(true);

            Maps[i - 1].OnNextStage(OpenMap, false);
            Maps[i - 1].extendMap.SetActive(false);
            break;
        }
        
        mapCamera.MapSizeCheck(Maps);
    }
}
