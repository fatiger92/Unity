using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BGScrollData
{
    public Renderer RenderForScoll;
    public float Speed;
    public float OffsetX;
}

public class BGScroller : MonoBehaviour
{
    [SerializeField] private BGScrollData[] ScrollDatas;

    private void Update()
    {
        UpdateScroll();
    }

    private void UpdateScroll()
    {
        for (var i = 0; i < ScrollDatas.Length; i++)
        {
            SetTextureOffset(ScrollDatas[i]);
        }
    }

    private void SetTextureOffset(BGScrollData scrollData)
    {
        scrollData.OffsetX += scrollData.Speed * Time.deltaTime;
        
        if (scrollData.OffsetX > 1) 
            scrollData.OffsetX %= 1f;
        
        var Offset = new Vector2(scrollData.OffsetX, 0);
        scrollData.RenderForScoll.material.SetTextureOffset("_MainTex", Offset);
    }
}
