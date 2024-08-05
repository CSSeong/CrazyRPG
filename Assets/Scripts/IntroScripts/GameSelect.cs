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
    [SerializeField]
    private GameObject AchievementPanel;
    [SerializeField]
    private Button StartButton;

    private int selectedSlot = -1;

    private void Awake()
    {
        // 초기화 시 저장된 데이터 유무에 따라 슬롯 텍스트를 설정합니다.
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(SaveManager.instance.path + $"/saveSlot_{i}.json"))
            {
                savefile[i] = true;
                slotTexts[i].text = $"게임 {i + 1}";
            }
            else
            {
                savefile[i] = false;
                slotTexts[i].text = "비어있음";
            }
        }
        AchievementPanel.SetActive(false);
    }

    public void Slot(int number)
    {
        selectedSlot = number;
        AchievementPanel.SetActive(true);
        StartButton.onClick.RemoveAllListeners();
        StartButton.onClick.AddListener(() => ConfirmStartGame());

        // Update AbilityManager with the selected slot
        AbilityManager.instance.SetSelectedSlot(selectedSlot);
        AchievementManager.instance.SetSelectedSlot(selectedSlot);
    }

    public void ConfirmStartGame()
    {
        if (selectedSlot == -1) return;

        SaveManager.instance.nowSlot = selectedSlot;
        if (savefile[selectedSlot])
        {
            SaveManager.instance.LoadData();
            GoGame();
        }
        else
        {
            SceneManager.LoadScene(0);
            SaveManager.instance.SaveData();
            savefile[selectedSlot] = true; // 저장된 데이터가 있음을 표시
            slotTexts[selectedSlot].text = $"게임 {selectedSlot + 1}"; // UI 업데이트
        }

        AchievementPanel.SetActive(false); // 확인 창 비활성화
        selectedSlot = -1;
    }

    public void CancelStartGame()
    {
        AchievementPanel.SetActive(false); // 확인 창 비활성화
        selectedSlot = -1;
    }

    public void GoGame()
    {
        // 이동하기 전에 데이터가 없으면 저장하고 메시지 출력
        if (!savefile[SaveManager.instance.nowSlot])
        {
            SaveManager.instance.SaveData();
            Debug.Log("데이터가 저장되었습니다.");
        }
        SceneManager.LoadScene(SaveManager.instance.nowPlayer.savedSceneIndex);
    }

    public void GameDelete(int number)
    {
        SaveManager.instance.nowSlot = number;

        string filePath = SaveManager.instance.path + $"/saveSlot_{number}.json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("데이터 삭제 완료: " + filePath);
            savefile[number] = false; // 저장된 데이터가 없음을 표시
            slotTexts[number].text = "비어있음"; // UI 업데이트
        }
        else
        {
            Debug.Log("삭제할 데이터가 없습니다: " + filePath);
        }
    }
}