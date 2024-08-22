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
    public int SP = 0;
    public List<InventorySlotData> inventorySlots = new List<InventorySlotData>();
    public List<AbilitySlotData> abilitySlots = new List<AbilitySlotData>();
    public List<AchievementData> achievements = new List<AchievementData>();
    public Dictionary<int, bool> abilityCooldowns = new Dictionary<int, bool>();
    public float moveSpeed = 4.5f;
    public float jumpForce = 10;
    public int boxAcquisition = 0;
    public int useItemCount = 0;
    public int curseCount = 0;
    public int gameClearCount = 0;

    public void Reset()
    {
        coin = 0;
        playerHP = playerHP_max = 100;
        playerlightgage = playerlightgage_max = 100;
        savedSceneIndex = 0;
        SP = 0;
        inventorySlots.Clear();
        abilitySlots.Clear();
        achievements.Clear();
        abilityCooldowns.Clear();
        moveSpeed = 4.5f;
        jumpForce = 10;
        boxAcquisition = 0;
        useItemCount = 0;
        curseCount = 0;
        gameClearCount = 0;
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
    public bool isAvailable;
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

        path = Path.Combine(Application.persistentDataPath, "save");
        Debug.Log("Save path: " + path);
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

        string filePath = Path.Combine(path, $"saveSlot_{nowSlot}.json");
        string data = JsonUtility.ToJson(nowPlayer, true);
        Debug.Log("저장 파일 경로: " + filePath);
        File.WriteAllText(filePath, data);
    }

    public void UpdateAbilityCooldown(int abilityNumber, bool isOnCooldown)
    {
        nowPlayer.abilityCooldowns[abilityNumber] = isOnCooldown;
    }

    public bool IsAbilityOnCooldown(int abilityNumber)
    {
        return nowPlayer.abilityCooldowns.TryGetValue(abilityNumber, out bool isOnCooldown) && isOnCooldown;
    }

    public void UpdateAbilityAvailability(int abilityNumber, bool availability)
    {
        foreach (var slot in nowPlayer.abilitySlots)
        {
            foreach (var ability in slot.abilities)
            {
                if (ability.abilityNumber == abilityNumber)
                {
                    ability.isUnlocked = availability;
                    return;
                }
            }
        }

        Debug.LogWarning("해당 능력을 찾을 수 없습니다: " + abilityNumber);
    }

    public void LoadData()
    {
        nowPlayer.Reset();

        string filePath = Path.Combine(path, $"saveSlot_{nowSlot}.json");
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            nowPlayer = JsonUtility.FromJson<GameData>(data);

            InventoryMain.Instance?.SetInventoryData(nowPlayer.inventorySlots);
            abilityManager?.SetAbilitySlotsData(nowPlayer.abilitySlots);
            achievementManager?.SetAchievementsData(nowPlayer.achievements);

            // 태그를 사용하여 플레이어 오브젝트를 찾고 설정하기
            var playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                var player = playerObject.GetComponent<Player>();
                if (player != null)
                {
                    var movement = player.Movement2D;
                    if (movement != null)
                    {
                        movement.SetMoveSpeed(nowPlayer.moveSpeed);
                        movement.SetJumpForce(nowPlayer.jumpForce);
                    }

                    var playerHP = player.PlayerHP;
                    if (playerHP != null)
                    {
                        playerHP.MaxHP = nowPlayer.playerHP_max;
                        playerHP.CurrentHP = nowPlayer.playerHP;
                    }

                    var playerLight = player.PlayerLight;
                    if (playerLight != null)
                    {
                        playerLight.MaxLightGage = nowPlayer.playerlightgage_max;
                        playerLight.CurrentLightGage = nowPlayer.playerlightgage;
                    }
                }
            }

            achievementManager?.UpdateSPText();
        }
        else
        {
            Debug.LogError("저장된 파일을 찾을 수 없습니다: " + filePath);
        }

        ApplyAbilitiesAfterLoad();
    }

    private void ApplyAbilitiesAfterLoad()
    {
        if (abilityManager == null) return;

        foreach (var slot in nowPlayer.abilitySlots)
        {
            foreach (var ability in slot.abilities)
            {
                if (IsAbilityOnCooldown(ability.abilityNumber))
                {
                    ability.isOnCooldown = true;
                    abilityManager.StartCoroutine(abilityManager.ActivateAbilityCoroutine(ability)); // Use AbilityManager's coroutine
                }
                else if (ability.isUnlocked && ability.abilityNumber == 5)
                {
                    ability.ApplyEffect(); // 씬 전환 후 이동 속도 증가 효과 재적용
                    abilityManager.StartCoroutine(abilityManager.ActivateAbilityCoroutine(ability)); // Use AbilityManager's coroutine
                }
            }
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

            achievementManager?.ResetAchievements();
            abilityManager?.ResetAbilities();
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
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            var movement = playerObject.GetComponent<Movement2D>();
            if (movement != null)
            {
                movement.SetMoveSpeed(newSpeed); // 이동 속도 적용
            }
        }
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

    public void IncrementBoxAcquisition()
    {
        nowPlayer.boxAcquisition++; // 보물상자 획득 횟수 증가
        SaveData(); // 데이터 저장
        CheckAchievements(); // 업적 확인
    }

    public void IncrementUseItemCount()
    {
        nowPlayer.useItemCount++; // 아이템 사용 횟수 증가
        SaveData(); // 데이터 저장
        CheckAchievements(); // 업적 확인
    }

    public void IncrementCurseCount()
    {
        nowPlayer.curseCount++; // 저주 횟수 증가
        SaveData(); // 데이터 저장
        CheckAchievements(); // 업적 확인
    }

    public void IncrementGameClearCount()
    {
        nowPlayer.gameClearCount++; // 게임 클리어 횟수 증가
        SaveData(); // 데이터 저장
        CheckAchievements(); // 업적 확인
    }

    private void CheckAchievements()
    {
        // 업적 체크 및 달성
        if (nowPlayer.gameClearCount >= 1) achievementManager.UnlockAchievement("게임 1회 클리어");
        if (nowPlayer.gameClearCount >= 2) achievementManager.UnlockAchievement("게임 2회 클리어");
        if (nowPlayer.gameClearCount >= 3) achievementManager.UnlockAchievement("게임 3회 클리어");
        if (nowPlayer.gameClearCount >= 5) achievementManager.UnlockAchievement("게임 5회 클리어");

        if (nowPlayer.boxAcquisition >= 20) achievementManager.UnlockAchievement("보물상자 20개 획득");
        if (nowPlayer.boxAcquisition >= 40) achievementManager.UnlockAchievement("보물상자 40개 획득");
        if (nowPlayer.boxAcquisition >= 60) achievementManager.UnlockAchievement("보물상자 60개 획득");

        if (nowPlayer.useItemCount >= 10) achievementManager.UnlockAchievement("아이템 사용 횟수 10회");
        if (nowPlayer.useItemCount >= 20) achievementManager.UnlockAchievement("아이템 사용 횟수 20회");
        if (nowPlayer.useItemCount >= 30) achievementManager.UnlockAchievement("아이템 사용 횟수 30회");

        if (nowPlayer.curseCount >= 3) achievementManager.UnlockAchievement("저주 3회 획득");
    }
}