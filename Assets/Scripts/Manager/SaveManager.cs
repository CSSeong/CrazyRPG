using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public int coin = 0;
    public float playerHP = 100;
    public float playerHP_max = 100;
    public float playerlightgage = 100;
    public float playerlightgage_max = 100;
    public int savedSceneIndex = 0;
    public int SP = 30;
    public List<InventorySlotData> inventorySlots = new List<InventorySlotData>();
    public List<AbilitySlotData> abilitySlots = new List<AbilitySlotData>();
    public List<AchievementData> achievements = new List<AchievementData>();
    public float moveSpeed = 4.5f;
    public float jumpForce = 10;

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
        abilitySlots.Clear();
        achievements.Clear();
        moveSpeed = 4.5f;
        jumpForce = 10;
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
    public Movement2D playerMovement2D;

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
            Debug.LogError("필수 컴포넌트가 null입니다");
            return;
        }

        nowPlayer.inventorySlots = InventoryMain.Instance.GetInventoryData();
        nowPlayer.abilitySlots = abilityManager.GetAbilitySlotsData();
        nowPlayer.achievements = achievementManager.GetAchievementsData();

        string filename = $"saveSlot_{nowSlot}.json";
        string filePath = Path.Combine(path, filename);

        string data = JsonUtility.ToJson(nowPlayer, true);
        Debug.Log("저장 파일 경로: " + filePath);
        File.WriteAllText(filePath, data);
    }

    public void LoadData()
    {
        nowPlayer.Reset();

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
                abilityManager.SetAbilitySlotsData(nowPlayer.abilitySlots);
            }
            if (achievementManager != null)
            {
                achievementManager.SetAchievementsData(nowPlayer.achievements);
            }

            if (playerMovement2D != null)
            {
                playerMovement2D.SetMoveSpeed(nowPlayer.moveSpeed);
                playerMovement2D.SetJumpForce(nowPlayer.jumpForce);
            }

            if (achievementManager != null)
            {
                achievementManager.UpdateSPText();
            }
        }
        else
        {
            Debug.LogError("저장된 파일을 찾을 수 없습니다: " + filePath);
        }
    }

    public void DeleteData()
    {
        string filePath = Path.Combine(path, $"saveSlot_{nowSlot}.json");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("데이터 삭제 완료: " + filePath);
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
            Debug.Log("삭제할 데이터가 없습니다: " + filePath);
        }
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new GameData();
    }

    public void UpdateMoveSpeed(float newSpeed)
    {
        nowPlayer.moveSpeed = newSpeed;
    }

    public void UpdateJumpForce(float newJumpForce)
    {
        nowPlayer.jumpForce = newJumpForce;
    }

    public void UpdatePlayerLightGage(float increment)
    {
        nowPlayer.playerlightgage_max += increment;
        nowPlayer.playerlightgage += increment;
    }

    public void UpdatePlayerHP(float increment)
    {
        nowPlayer.playerHP_max += increment;
        nowPlayer.playerHP += increment;
    }
}

