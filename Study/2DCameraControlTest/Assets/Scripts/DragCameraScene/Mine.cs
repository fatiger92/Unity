using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
   [SerializeField] SpriteButton btn_Next;
   
   public GameObject extendMap;
   public SpriteRenderer mapSr;
   public SpriteRenderer extendMapSr;

   public float MapBoundX => mapSr.sprite.bounds.extents.x;
   public float MapBoundY => mapSr.sprite.bounds.extents.y;
   
  //public float ExtendMapboundsX => extendMapSr.sprite.bounds.extents.x;
   public float ExtendMapboundsY => extendMapSr.sprite.bounds.extents.y;

   void Awake()
   {
      mapSr = GetComponent<SpriteRenderer>();
      extendMapSr = extendMap.GetComponent<SpriteRenderer>();
   }

   public void OnNextStage(UnityAction action, bool isAttach)
   {
      if(!isAttach)
         btn_Next.DetachClickEvent(action);
      else
         btn_Next.AttachClickEvent(action);
   }
}
