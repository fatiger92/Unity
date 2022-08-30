using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePointAccumulator
{
   private int gamePoint = 0;
   public int GamePoint => gamePoint;

   public void Accumulate(int value)
   {
      gamePoint += value;
      Debug.Log($"Accumulate gamePoint = {gamePoint}");
   }

   public void Reset()
   {
      gamePoint = 0;
   }
}
