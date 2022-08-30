using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
   [SerializeField] protected int MaxHP = 100;
   [SerializeField] protected int CurrentHP;
   [SerializeField] protected int Damage = 1;
   [SerializeField] protected int crashDamage = 100;
   [SerializeField] private bool isDead = false;
   public bool IsDead => isDead;
   public int CrashDamage => crashDamage;
   
   private void Start()
   {
      Initalize();
   }

   protected virtual void Initalize()
   {
      CurrentHP = MaxHP;
   }

   private void Update()
   {
      UpdateActor();
   }

   protected virtual void UpdateActor()
   {
      
   }

   public virtual void OnBulletHited(Actor attacker, int damage)
   {
      Debug.Log($"OnBulletHited attacker = {attacker.name} damage = {damage}");
      DecreaseHP(attacker, damage);
   }

   public virtual void OnCrash(Actor attacker, int damage)
   {
      Debug.Log($"OnCrash attacker = {attacker.name} damage = {damage}");
      DecreaseHP(attacker, damage);
   }

   private void DecreaseHP(Actor attacker, int value)
   {
      if (isDead)
         return;

      CurrentHP -= value;

      if (CurrentHP < 0)
         CurrentHP = 0;

      if (CurrentHP == 0)
      {
         OnDead(attacker);
      }
   }

   protected virtual void OnDead(Actor killer)   
   {
      Debug.Log($"{name} OnDead");
      isDead = true;
      
      SystemManager.Instance.EffectManager.GenerateEffect(1, transform.position);
   }
}
