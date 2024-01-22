using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
   public int healthToAdd;
   public GameObject pickupEffect;

   public bool giveFullHealth;
   
   void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         var inst = PlayerHealthController.instance;
         
         if (inst.currentHealth < inst.maxHealth)
         {
            inst.AddHealth(giveFullHealth == true ? inst.maxHealth : healthToAdd);
            Destroy(gameObject);
            Instantiate(pickupEffect, transform.position, transform.rotation);
            
            AudioManager.instance.PlaySFX(10);
         }
      }
   }
}
