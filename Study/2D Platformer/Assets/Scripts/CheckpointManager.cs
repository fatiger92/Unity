using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Checkpoint[] allCP;
    
    Checkpoint activeCP;
    public Vector3 respawnPosition;
    
    void Start()
    {
        //allCP = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None); // 왜 이걸 쓰는지 모르겠네
        allCP = GetComponentsInChildren<Checkpoint>();

        foreach (var checkpoint in allCP)
            checkpoint.cm = this;

        respawnPosition = FindFirstObjectByType<PlayerController>().transform.position;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
            DeactivateAllCheckpoints();
        }
#endif
    }

    public void DeactivateAllCheckpoints()
    {
        foreach (Checkpoint cp in allCP)
        {
            cp.DeactivateCheckpoint();
        }
    }

    public void SetActiveCheckpoint(Checkpoint newActiveCP)
    {
        // 사실 체크포인트는 이전에 activating 된 것도 그대로 있어야함 따라서 아래 DeactivateAllCheckpoints() 호출을 지우고,
        // 따로 현재 체크포인트를 표시하는 무언가를 넣어주는게 조금 더 Reasonable 하다.
        DeactivateAllCheckpoints();
        activeCP = newActiveCP;

        respawnPosition = newActiveCP.transform.position;
    }
}
