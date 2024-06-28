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
            if (File.Exists(SaveManager.instance.path + $"{i}"))	// �����Ͱ� �ִ� ���
            {
                savefile[i] = true;			// �ش� ���� ��ȣ�� bool�迭 true�� ��ȯ
                SaveManager.instance.nowSlot = i;	// ������ ���� ��ȣ ����
                SaveManager.instance.LoadData();	// �ش� ���� ������ �ҷ���
                slotTexts[i].text = ("����" + (i + 1));
            }
            else	// �����Ͱ� ���� ���
            {
                slotTexts[i].text = "�������";
            }
        }
        SaveManager.instance.DataClear();
    }

    public void Slot(int number)
    {
        SaveManager.instance.nowSlot = number;
        if (savefile[number])	// bool �迭���� ���� ���Թ�ȣ�� true��� = ������ �����Ѵٴ� ��
        {
            SaveManager.instance.LoadData();	// �����͸� �ε��ϰ�
            GoGame();	// ���Ӿ����� �̵�
        }
        else
        {
            SceneManager.LoadScene(0);
            SaveManager.instance.SaveData();
        }
    }

    public void GoGame()	// ���Ӿ����� �̵�
    {
        if (!savefile[SaveManager.instance.nowSlot])    // ���� ���Թ�ȣ�� �����Ͱ� ���ٸ�
        {
            SaveManager.instance.SaveData();
            Debug.Log("�����Ͱ� ����Ǿ����ϴ�.");
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
            Debug.Log("������ ���� �Ϸ�: " + filePath);
            slotTexts[number].text = "�������"; // UI ������Ʈ: �ش� ������ ����������� ǥ��
            savefile[number] = false; // �����Ͱ� ������ ǥ���ϱ� ���� savefile �迭 ������Ʈ
        }
        else
        {
            Debug.Log("������ �����Ͱ� �����ϴ�: " + filePath);
        }
    }

}
