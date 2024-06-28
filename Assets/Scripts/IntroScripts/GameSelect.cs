using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class GameSelect : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] slotTexts;
    [SerializeField]
    private bool[] savefile;

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(SaveManager.instance.path + $"{i}"))	// 데이터가 있는 경우
            {
                savefile[i] = true;			// 해당 슬롯 번호의 bool배열 true로 변환
                SaveManager.instance.nowSlot = i;	// 선택한 슬롯 번호 저장
                SaveManager.instance.LoadData();	// 해당 슬롯 데이터 불러옴
                slotTexts[i].text = ("게임" + (i + 1));
            }
            else	// 데이터가 없는 경우
            {
                slotTexts[i].text = "비어있음";
            }
        }
        SaveManager.instance.DataClear();
    }

    public void Slot(int number)
    {
        SaveManager.instance.nowSlot = number;
        if (savefile[number])	// bool 배열에서 현재 슬롯번호가 true라면 = 데이터 존재한다는 뜻
        {
            SaveManager.instance.LoadData();	// 데이터를 로드하고
            GoGame();	// 게임씬으로 이동
        }
        else
        {
            SceneManager.LoadScene(0);
            SaveManager.instance.SaveData();
        }
    }

    public void GoGame()	// 게임씬으로 이동
    {
        if (!savefile[SaveManager.instance.nowSlot])    // 현재 슬롯번호의 데이터가 없다면
        {
            SaveManager.instance.SaveData();
            Debug.Log("데이터가 저장되었습니다.");
        }
        SceneManager.LoadScene(SaveManager.instance.nowPlayer.savedSceneIndex);
    }

    public void GameDelete(int number)
    {
        SaveManager.instance.nowSlot = number;

        string filePath = SaveManager.instance.path + number.ToString();

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("데이터 삭제 완료: " + filePath);
            slotTexts[number].text = "비어있음"; // UI 업데이트: 해당 슬롯을 비어있음으로 표시
            savefile[number] = false; // 데이터가 없음을 표시하기 위해 savefile 배열 업데이트
        }
        else
        {
            Debug.Log("삭제할 데이터가 없습니다: " + filePath);
        }
    }

}
