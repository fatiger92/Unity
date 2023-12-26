using UnityEngine;
using System.IO;

// 저장하는 방법
// 1. 저장할 데이터가 존재
// 2. 데이터를 Json으로 변환
// 3. Json을 외부에 저장

// 불러오는 방법
// 1. 외부에 저장된 Json을 가져옴
// 2. Json을 데이터형태로 변환
// 3. 불러온 데이터를 사용

// 슬롯별로 다르게 저장

public class PlayerData
{
    // 이름, 레벨, 코인, 착용중인 무기
    public string name;
    public int level = 1;
    public int coin = 100;
    public int item = 0;
}

public class DataManager : MonoBehaviour
{
    // 싱글톤
    public static DataManager instance;

    public PlayerData nowPlayer = new(){ name = "빅터", level = 30, coin = 200, item = 10 };

    public string path = Application.dataPath + "/Save/";
    public int nowSlot; // 현재 슬롯 번호
    
    void Awake()
    {
        #region Singleton
        if (instance == null)
            instance = this;
        else if(instance != this)
            Destroy(instance.gameObject);
        
        DontDestroyOnLoad(gameObject);
        #endregion
    }
    
    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);

        if (!File.Exists(path))
            Directory.CreateDirectory(path);
        
        File.WriteAllText(path + nowSlot + ".txt",data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot + ".txt");
        nowPlayer =  JsonUtility.FromJson<PlayerData>(data);
    }

    public void ClearData()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
