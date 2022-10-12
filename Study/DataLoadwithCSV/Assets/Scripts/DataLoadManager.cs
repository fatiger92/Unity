using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoadManager : MonoBehaviour
{
   [SerializeField] private Transform _squadronTr;
   [SerializeField] private SquadronTable[] _squadronDatas;
   [SerializeField] private SquadronScheduleTable _squadronScheduleTable;

   [SerializeField] private EnemyTable _enemyTable;

   void Start()
   {
      // _squadronDatas = _squadronTr.GetComponentsInChildren<SquadronTable>();
      //
      // for (int i = 0; i < _squadronDatas.Length; i++)
      //    _squadronDatas[i].Load();
      //
      // _squadronScheduleTable.Load();
      _enemyTable.Load();
   }
}
