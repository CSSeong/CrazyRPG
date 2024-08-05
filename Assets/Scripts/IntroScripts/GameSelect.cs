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
        // �ʱ�ȭ �� ����� ������ ������ ���� ���� �ؽ�Ʈ�� �����մϴ�.
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(SaveManager.instance.path + $"/saveSlot_{i}.json"))
            {
                savefile[i] = true;
                slotTexts[i].text = $"���� {i + 1}";
            }
            else
            {
                savefile[i] = false;
                slotTexts[i].text = "�������";
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
            savefile[selectedSlot] = true; // ����� �����Ͱ� ������ ǥ��
            slotTexts[selectedSlot].text = $"���� {selectedSlot + 1}"; // UI ������Ʈ
        }

        AchievementPanel.SetActive(false); // Ȯ�� â ��Ȱ��ȭ
        selectedSlot = -1;
    }

    public void CancelStartGame()
    {
        AchievementPanel.SetActive(false); // Ȯ�� â ��Ȱ��ȭ
        selectedSlot = -1;
    }

    public void GoGame()
    {
        // �̵��ϱ� ���� �����Ͱ� ������ �����ϰ� �޽��� ���
        if (!savefile[SaveManager.instance.nowSlot])
        {
            SaveManager.instance.SaveData();
            Debug.Log("�����Ͱ� ����Ǿ����ϴ�.");
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
            Debug.Log("������ ���� �Ϸ�: " + filePath);
            savefile[number] = false; // ����� �����Ͱ� ������ ǥ��
            slotTexts[number].text = "�������"; // UI ������Ʈ
        }
        else
        {
            Debug.Log("������ �����Ͱ� �����ϴ�: " + filePath);
        }
    }
}