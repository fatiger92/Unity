using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

[System.Serializable]
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct EnemyStruct
{
    public int      index; // 4 byte
    // 문자열은 그 크기를 가늠할 수 없기 때문에 최대 사이즈를 정해줘야함.
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MarshalTableConstant.charBufferSize)]
    public string   FilePath; // 256 byte
    public int      MaxHP; // 4 byte
    public int      Damage; // 4 byte
    public int      CrashDamage; // 4 byte
    public int      BulletSpeed; // 4 byte
    public int      FireRemainCount; // 4 byte
    public int      GamePoint; // 4 byte
    // Total byte =  256 + 4 * 7 = 284 byte 
}

public class EnemyTable : TableLoader<EnemyStruct> // TableLoader<EnemyStruct> 형을 상속 받음.
{
    Dictionary<int, EnemyStruct> tableDatas = new(); // 데이터 담아 놓을 곳.

    protected override void AddData(EnemyStruct data)
    {
        tableDatas.Add(data.index, data);
    }
    
    
    public EnemyStruct GetEnemy(int index)
    {
        if(!tableDatas.ContainsKey(index))
        {
            Debug.LogError("GetEnemy Error! index = " + index);
            return default;
        }
        return tableDatas[index];
    }

    [ContextMenu("테이블 값 뭐있지?")]
    public void PrintDatas()
    {
        Debug.Log("EnemyTable");
        foreach (var element in tableDatas)
        {
            Debug.Log($"element index :: {element.Value.index}, FilePath :: {element.Value.FilePath}");
        }
    }
}