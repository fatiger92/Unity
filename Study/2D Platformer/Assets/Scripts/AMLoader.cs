using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMLoader : MonoBehaviour
{
    public AudioManager theAM;

    void Awake()
    {
        if (AudioManager.instance == null)
        {
            // 이래 봤자 그냥 연속적으로 SetupAudioManager() 2번 호출함 순서 안바뀜
            Instantiate(theAM).SetupAudioManager();
        }
    }
}
