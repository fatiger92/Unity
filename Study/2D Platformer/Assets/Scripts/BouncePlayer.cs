using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlayer : MonoBehaviour
{
    public float bounceAmount;

    public Animator anim;
    static readonly int ANIM_PARAM_BOUNCE = Animator.StringToHash("bounce");

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger(ANIM_PARAM_BOUNCE);
            
            other.GetComponent<PlayerController>().BouncePlayer(bounceAmount);
        }
    }
}
