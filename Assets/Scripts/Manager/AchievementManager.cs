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
        // ���� �ε� ���� (��: �����̳� �����ͺ��̽����� �ҷ�����)
    }

    private void DisplayAchievements()
    {
        for(int i = 0; i < achievements.Count; i++)
        {
            achievementDes[i].text = achievements[i].description;
            achievementReward[i].text = $"����: SP {achievements[i].achievementSP}";
        }
    }

    private void SaveAchievements()
    {
        // ���� ���� ���� (��: �����̳� �����ͺ��̽��� ����)
    }
}
