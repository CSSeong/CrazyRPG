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

    private void Start()
    {
        DisplayAchievements();
    }

    public void UnlockAchievement(string achievementName)
    {
        Achievement achievement = achievements.Find(a => a.achievementName == achievementName);
        if (achievement != null)
        {
            achievement.Unlock();
        }
        else
        {
            Debug.LogWarning($"Achievement {achievementName} not found");
        }
    }

    private void LoadAchievements()
    {
        // 업적 로드 로직 (예: 파일이나 데이터베이스에서 불러오기)
    }

    private void DisplayAchievements()
    {
        for(int i = 0; i < achievements.Count; i++)
        {
            achievementDes[i].text = achievements[i].description;
            achievementReward[i].text = $"보상: SP {achievements[i].achievementSP}";
        }
    }

    private void SaveAchievements()
    {
        // 업적 저장 로직 (예: 파일이나 데이터베이스에 저장)
    }
}
