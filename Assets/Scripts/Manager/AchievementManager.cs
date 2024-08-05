using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    public List<AchievementSlotData> achievementSlots; // 여러 슬롯을 관리
    public int selectedSlotIndex = 0;

    [SerializeField]
    private TextMeshProUGUI[] achievementDes;
    [SerializeField]
    private TextMeshProUGUI[] achievementReward;
    [SerializeField]
    private Button[] claimButtons;
    [SerializeField]
    private TextMeshProUGUI SP;


    private void Start()
    {
        DisplayAchievements();
        InitializeButtons();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        UpdateSPText();
    }

    public void UpdateSPText()
    {
        SP.text = $"보유 SP: {SaveManager.instance.nowPlayer.SP}";
    }

    public void UnlockAchievement(string achievementName)
    {
        var achievements = achievementSlots[selectedSlotIndex].achievements;
        Achievement achievement = achievements.Find(a => a.achievementName == achievementName);
        if (achievement != null)
        {
            achievement.Unlock();
            int index = achievements.IndexOf(achievement);
            claimButtons[index].interactable = true;
        }
        else
        {
            Debug.LogWarning($"Achievement {achievementName} not found in slot {selectedSlotIndex}");
        }
    }

    private void DisplayAchievements()
    {
        var achievements = achievementSlots[selectedSlotIndex].achievements;
        for (int i = 0; i < achievements.Count; i++)
        {
            achievementDes[i].text = achievements[i].description;
            achievementReward[i].text = $"보상: SP {achievements[i].achievementSP}";
        }
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < claimButtons.Length; i++)
        {
            int index = i;
            claimButtons[i].onClick.AddListener(() => ClaimReward(index));
            claimButtons[i].interactable = false;
        }
    }

    private void ClaimReward(int index)
    {
        var achievements = achievementSlots[selectedSlotIndex].achievements;
        if (index >= 0 && index < achievements.Count)
        {
            Achievement achievement = achievements[index];
            if (achievement != null && achievement.IsUnlocked())
            {
                SaveManager.instance.nowPlayer.SP += achievement.achievementSP;
                Debug.Log($"SP {achievement.achievementSP} 획득");
                claimButtons[index].interactable = false;
                UpdateSPText();
                SaveManager.instance.SaveData(); // 데이터를 업데이트한 후 저장
            }
        }
    }

    public List<AchievementData> GetAchievementsData()
    {
        List<AchievementData> achievementDataList = new List<AchievementData>();
        var achievements = achievementSlots[selectedSlotIndex].achievements;
        foreach (var achievement in achievements)
        {
            achievementDataList.Add(new AchievementData
            {
                achievementName = achievement.achievementName,
                isUnlocked = achievement.IsUnlocked()
            });
        }
        return achievementDataList;
    }

    public void SetAchievementsData(List<AchievementData> achievementDataList)
    {
        var achievements = achievementSlots[selectedSlotIndex].achievements;
        foreach (var achievementData in achievementDataList)
        {
            Achievement achievement = achievements.Find(a => a.achievementName == achievementData.achievementName);
            if (achievement != null)
            {
                if (achievementData.isUnlocked)
                {
                    achievement.Unlock();
                }
                else
                {
                    achievement.Reset();
                }
            }
        }
        DisplayAchievements();
    }

    public void ResetAchievements()
    {
        var achievements = achievementSlots[selectedSlotIndex].achievements;
        foreach (var achievement in achievements)
        {
            achievement.Reset();
        }
        DisplayAchievements();
    }

    public void SetSelectedSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < achievementSlots.Count)
        {
            selectedSlotIndex = slotIndex;
            DisplayAchievements();
        }
        else
        {
            Debug.LogError("Invalid slot index.");
        }
    }
}
