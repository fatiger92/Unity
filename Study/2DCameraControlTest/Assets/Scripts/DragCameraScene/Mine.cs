using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
   [SerializeField] SpriteButton btn_Next;
   
   public GameObject _extendMineGo;
   public SpriteRenderer _mineSr;
   public SpriteRenderer _extendMineSr;

   public float MapBoundX => _mineSr.sprite.bounds.extents.x;
   public float MapBoundY => _mineSr.sprite.bounds.extents.y;
   
  //public float ExtendMapboundsX => extendMapSr.sprite.bounds.extents.x;
   public float ExtendMapboundsY => _extendMineSr.sprite.bounds.extents.y;

   void Initialize()
   {
      _mineSr = GetComponent<SpriteRenderer>();
      _extendMineSr = _extendMineGo.GetComponent<SpriteRenderer>();
   }
   
   void Awake()
   {
      Initialize();
   }

   public void OnNextStage(UnityAction action, bool isAttach)
   {
      if(!isAttach)
         btn_Next.DetachClickEvent(action);
      else
         btn_Next.AttachClickEvent(action);
   }
}
