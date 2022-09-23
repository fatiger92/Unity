using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SpriteButton : BaseButton
{

   SpriteRenderer sr;
   
   public override void UIinitial()
   {
      base.UIinitial();
      sr = GetComponent<SpriteRenderer>();
   }

   public override void OnMouseEnter()
   {
      base.OnMouseEnter();
   }
    
   public override void OnMouseDown()
   {
      base.OnMouseDown();
      
      var color = new Color();
      color = Color.gray;
      sr.color = color;
   }
    
   public override void OnMouseUp()
   {
      base.OnMouseUp();
      
      var color = new Color();
      color = Color.white;
      sr.color = color;
   }

   public override void OnMouseExit()
   {
      base.OnMouseExit();
   }
}
