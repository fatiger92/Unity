using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ButtonController : MonoBehaviour
{
    public SpriteButton btn_changeColor;
    public SpriteRenderer sr;
    
    void Awake()
    {
        btn_changeColor.AttachClickEvent(() =>
        {
            var randR = Random.Range(0f, 1f);
            var randG = Random.Range(0f, 1f);
            var randB = Random.Range(0f, 1f);
            
            var color = new Color(randR, randG, randB);
            sr.color = color;
        });
    }
}
