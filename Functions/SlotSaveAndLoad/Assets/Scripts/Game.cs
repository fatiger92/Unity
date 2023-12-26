using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    static readonly string strNameTitle = "Name : ";
    static readonly string strLevelTitle = "Level : ";
    static readonly string strCoinTitle = "Coin : ";
    static readonly string strWeaponTitle = "Weapon : ";
    
    
    public TextMeshProUGUI name;
    public TextMeshProUGUI level;
    public TextMeshProUGUI coin;
    public TextMeshProUGUI weapon;

    public Button btnInfo;
    public Button btnSave; 
    public Button btnLevelUp; 
    public Button btnCoinUp; 
    public Button btnEquipSword; 
    public Button btnEquipBazooka; 
    public Button btnEquipBomb; 
    
    void Start()
    {
        btnInfo.onClick.AddListener(CheckCurInfo);
        btnSave.onClick.AddListener(Save);
        btnLevelUp.onClick.AddListener(LevelUp);
        btnCoinUp.onClick.AddListener(CoinUp);
        btnEquipSword.onClick.AddListener(EquipSword);
        btnEquipBazooka.onClick.AddListener(EquipBazooka);
        btnEquipBomb.onClick.AddListener(EquipBomb);
        
        name.text = strNameTitle + DataManager.instance.nowPlayer.name;
        level.text = strLevelTitle + DataManager.instance.nowPlayer.level;
        coin.text = strCoinTitle + DataManager.instance.nowPlayer.coin;
        weapon.text = strWeaponTitle + CheckWeapon(DataManager.instance.nowPlayer.item);
    }
    
    public void LevelUp()
    {
        DataManager.instance.nowPlayer.level++;
        level.text = strLevelTitle + DataManager.instance.nowPlayer.level;
    }

    public void CoinUp()
    {
        DataManager.instance.nowPlayer.coin += 100;
        coin.text = strCoinTitle + DataManager.instance.nowPlayer.coin;
    }

    public void EquipSword()
    {
        Debug.Log("검을 장착하셨습니다.");
        weapon.text = strWeaponTitle + CheckWeapon(DataManager.instance.nowPlayer.item = 1);
    }

    public void EquipBazooka()
    {
        Debug.Log("바주카를 장착하셨습니다.");
        weapon.text = strWeaponTitle + CheckWeapon(DataManager.instance.nowPlayer.item = 2);
    }
    
    public void EquipBomb()
    {
        Debug.Log("폭탄을 장착하셨습니다.");
        weapon.text = strWeaponTitle + CheckWeapon(DataManager.instance.nowPlayer.item = 3);
    }

    public string CheckWeapon(int index)
    {
        switch (index)
        {
            default:
                return "Hands";
            case 1:
                return "Sword"; 
            case 2:
                return "Bazooka";
            case 3:
                return "Bomb";
        }
    }

    public void Save()
    {
        DataManager.instance.SaveData();
    }

    public void CheckCurInfo()
    {
        Debug.Log($"이름 :: {DataManager.instance.nowPlayer.name}");
        Debug.Log($"레벨 :: {DataManager.instance.nowPlayer.level}");
        Debug.Log($"코인 :: {DataManager.instance.nowPlayer.coin}");
        Debug.Log($"아이템 :: {CheckWeapon(DataManager.instance.nowPlayer.item)}");
    }
}
