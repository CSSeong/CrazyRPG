using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameData
{
    public int coin = 0;
    public float playerHP = 100;
    public float playerHP_max = 100;
    public float playerlightgage = 100;
    public float playerlightgage_max = 100;
    public int savedSceneIndex = 0;
    public int SP = 100;
    public List<InventorySlotData> inventorySlots = new List<InventorySlotData>();
    public List<AbilitySlotData> abilitySlots = new List<AbilitySlotData>(); // ������ �κ�
    public List<AchievementData> achievements = new List<AchievementData>();

    public void Reset()
    {
        coin = 0;
        playerHP = 100;
        playerHP_max = 100;
        playerlightgage = 100;
        playerlightgage_max = 100;
        savedSceneIndex = 0;
        SP = 0;
        inventorySlots.Clear();
        abilitySlots.Clear(); // ������ �κ�
        achievements.Clear();
    }
}

[System.Serializable]
public class InventorySlotData
{
    public int itemID;
    public int itemCount;
}

[System.Serializable]
public class AbilityData
{
    public string abilityName;
    public int level;
    public int requiredSP;
}

[System.Serializable]
public class AchievementData
{
    public string achievementName;
    public bool isUnlocked;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public GameData nowPlayer = new GameData();
    public string path;
    public int nowSlot;
    public AchievementManager achievementManager;
    public AbilityManager abilityManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        path = Application.persistentDataPath + "/save";
        Debug.Log(path);
    }

    public void SaveData()
    {
        if (InventoryMain.Instance == null || abilityManager == null || achievementManager == null)
        {
            Debug.LogError("�ʼ� ������Ʈ�� null�Դϴ�");
            return;
        }

        nowPlayer.inventorySlots = InventoryMain.Instance.GetInventoryData();
        nowPlayer.abilitySlots = abilityManager.GetAbilitySlotsData(); // ������ �κ�
        nowPlayer.achievements = achievementManager.GetAchievementsData();

        string filename = $"saveSlot_{nowSlot}.json";
        string filePath = Path.Combine(path, filename);

        string data = JsonUtility.ToJson(nowPlayer, true);
        Debug.Log("���� ���� ���: " + filePath);
        File.WriteAllText(filePath, data);
    }

    public void LoadData()
    {
        nowPlayer.Reset(); // ������ �ʱ�ȭ

        string filePath = Path.Combine(path, $"saveSlot_{nowSlot}.json");
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            nowPlayer = JsonUtility.FromJson<GameData>(data);

            if (InventoryMain.Instance != null)
            {
                InventoryMain.Instance.SetInventoryData(nowPlayer.inventorySlots);
            }
            if (abilityManager != null)
            {
                abilityManager.SetAbilitySlotsData(nowPlayer.abilitySlots); // ������ �κ�
            }
            if (achievementManager != null)
            {
                achievementManager.SetAchievementsData(nowPlayer.achievements);
            }

            // SP ���� �ҷ��� �� AchievementManager UI�� ������Ʈ�մϴ�.
            if (achievementManager != null)
            {
                achievementManager.UpdateSPText();
            }
        }
        else
        {
            Debug.LogError("����� ������ ã�� �� �����ϴ�: " + filePath);
        }
    }

    public void DeleteData()
    {
        string filePath = Path.Combine(path, $"saveSlot_{nowSlot}.json");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("������ ���� �Ϸ�: " + filePath);
            DataClear();

            if (achievementManager != null)
            {
                achievementManager.ResetAchievements();
            }
            if (abilityManager != null)
            {
                abilityManager.ResetAbilities();
            }
        }
        else
        {
            Debug.Log("������ �����Ͱ� �����ϴ�: " + filePath);
        }
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new GameData();
    }
}

