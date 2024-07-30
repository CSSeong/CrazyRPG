using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> achievements;

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

    private void Update()
    {
        SP.text = $"보유 SP: {SaveManager.instance.nowPlayer.SP}";
    }

    public void UnlockAchievement(string achievementName)
    {
        Achievement achievement = achievements.Find(a => a.achievementName == achievementName);
        if (achievement != null)
        {
            achievement.Unlock();
            int index = achievements.IndexOf(achievement);
            claimButtons[index].interactable = true;
        }
        else
        {
            Debug.LogWarning($"Achievement {achievementName} not found");
        }
    }

    private void DisplayAchievements()
    {
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
            claimButtons[i].interactable = false; // 초기에는 모든 버튼을 비활성화
        }
    }

    private void ClaimReward(int index)
    {
        if (index >= 0 && index < achievements.Count)
        {
            Achievement achievement = achievements[index];
            if (achievement != null && achievement.IsUnlocked())
            {
                SaveManager.instance.nowPlayer.SP += achievement.achievementSP;
                Debug.Log($"SP {achievement.achievementSP} 획득");
                claimButtons[index].interactable = false; // 버튼 비활성화
            }
        }
    }

    public List<AchievementData> GetAchievementsData()
    {
        List<AchievementData> achievementDataList = new List<AchievementData>();
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
        foreach (var achievement in achievements)
        {
            achievement.Reset();
        }
        DisplayAchievements();
    }
}
