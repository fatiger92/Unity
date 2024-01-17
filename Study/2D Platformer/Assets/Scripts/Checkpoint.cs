using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool isActive;
    public Animator anim;
    static readonly int ANIM_PARAM_FLAGACTIVE = Animator.StringToHash("flagActive");

    [HideInInspector] public CheckpointManager cm;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isActive == false)
        {
            cm.SetActiveCheckpoint(this);
            anim.SetBool(ANIM_PARAM_FLAGACTIVE, true);
            isActive = true;
        }
    }

    public void DeactivateCheckpoint()
    {
        anim.SetBool(ANIM_PARAM_FLAGACTIVE, false);
        isActive = false;
    }
}
