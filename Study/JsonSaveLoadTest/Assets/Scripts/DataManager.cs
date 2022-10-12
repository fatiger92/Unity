using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Data
{
    public Color color;
    public Vector3 pos;
    public int stage;
}

public class DataManager : MonoBehaviour
{
    public Data _data;
    
    [ContextMenu("저장")]
    public void SaveData()
    {
        Data data = new Data();
        
        data.color = _data.color;
        data.pos = _data.pos;
        data.stage = _data.stage;
    
        string json = JsonUtility.ToJson(data);
    
        File.WriteAllText( Application.dataPath + "/Resources/Data/" + "savefile.json",json);
    }
    
    [ContextMenu("불러오기")]
    public Data LoadData()
    {
        string path = Application.dataPath + "/Resources/Data/" + "savefile.json";
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Data data = JsonUtility.FromJson<Data>(json);
           
            Debug.Log(json);
            return data;
        }

        return null;
    }
}
