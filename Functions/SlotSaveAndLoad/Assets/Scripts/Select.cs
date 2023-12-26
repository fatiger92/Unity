using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
    
    public TextMeshProUGUI[] slotText;

    public TMP_InputField InputField;
    
    bool[] savefile = new bool[3];

    public Button[] btnCreate = new Button[3];
    
    public GameObject create;
    public TextMeshProUGUI inputTitle;
    public Button btnConfirm;
    
    void Start()
    {
        for (int i = 0; i < btnCreate.Length; i++)
        {
            var capturedIndex = i;
            btnCreate[i].onClick.AddListener(() => { OpenCreateView(capturedIndex); });
        }

        btnConfirm.onClick.AddListener(Confirm);
        
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}.txt")) // 데이터가 있는 경우
            {
                savefile[i] = true;                 // 해당 슬롯 번호의 bool 배열 true로 변환
                DataManager.instance.nowSlot = i;   // 선택한 슬롯 번호 저장
                DataManager.instance.LoadData();    // 해당 슬롯 데이터 불러옴
                slotText[i].text = DataManager.instance.nowPlayer.name; // 버튼에 닉네임 표시
            }
            else
            {
                slotText[i].text = "Empty";
            }
        }
        
        // 불러온 데이터를 초기화시킴. (버튼에 닉네임을 표현하기 위함이었기 때문)
        DataManager.instance.ClearData();
    }

    public void OpenCreateView(int index)
    {
        DataManager.instance.nowSlot = index;
        inputTitle.text = $"slot {index}, Input";

        if (savefile[index])
        {
            DataManager.instance.LoadData();
            SceneManager.LoadScene("Game");
        }
        else
        {
            create.SetActive(true);
        }
    }

    public void Confirm()
    {
        var text = InputField.text;
        
        if (string.IsNullOrEmpty(text))
        {
            DataManager.instance.nowSlot = 0;
            create.SetActive(false);
        }
        else
        {
            DataManager.instance.nowPlayer.name = InputField.text;
            DataManager.instance.SaveData();
            create.SetActive(false);

            SceneManager.LoadScene("Game");
        }
    }
}
