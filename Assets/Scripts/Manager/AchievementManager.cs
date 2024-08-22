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
        FindSPText();
        UpdateSPText();
    }

    public void UpdateSPText()
    {
        if (SP != null)
        {
            SP.text = $"보유 SP: {SaveManager.instance.nowPlayer.SP}";
        }
        else
        {
            Debug.LogWarning("SP 텍스트가 설정되지 않았습니다.");
        }
    }

    private void FindSPText()
    {
        // "SP" 태그를 가진 게임 오브젝트에서 TextMeshProUGUI 컴포넌트를 찾습니다.
        SP = GameObject.FindGameObjectWithTag("SP")?.GetComponent<TextMeshProUGUI>();
        if (SP == null)
        {
            Debug.LogWarning("SP 텍스트 오브젝트를 찾을 수 없습니다.");
        }
    }

    public void UnlockAchievement(string achievementName)
    {
        var achievements = achievementSlots[selectedSlotIndex].achievements;
        Achievement achievement = achievements.Find(a => a.achievementName == achievementName);
        if (achievement != null)
        {
            achievement.Unlock();

            // 해당 업적에 대한 보상 버튼 활성화
            claimButtons[achievements.IndexOf(achievement)].interactable = true;

            // UI 갱신
            DisplayAchievements();
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
            Achievement achievement = achievements[i];
            achievementDes[i].text = achievement.description;
            achievementReward[i].text = $"보상: SP {achievement.achievementSP}";

            // 버튼 텍스트 업데이트
            TextMeshProUGUI buttonText = claimButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                if (achievement.IsUnlocked())
                {
                    if (achievement.isAcquire)
                    {
                        buttonText.text = "완료";
                        claimButtons[i].interactable = false;
                    }
                    else
                    {
                        buttonText.text = "보상 받기";
                        claimButtons[i].interactable = true;
                    }
                }
                else
                {
                    buttonText.text = "보상 받기";
                    claimButtons[i].interactable = false;
                }
            }
            else
            {
                Debug.LogWarning("Button TextMeshProUGUI component not found.");
            }
        }
    }


    private void InitializeButtons()
    {
        for (int i = 0; i < claimButtons.Length; i++)
        {
            int index = i;
            claimButtons[i].onClick.AddListener(() => ClaimReward(index));
            claimButtons[i].interactable = false;  // 기본적으로 버튼을 비활성화
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
                // 보상 지급
                SaveManager.instance.nowPlayer.SP += achievement.achievementSP;
                Debug.Log($"SP {achievement.achievementSP} 획득");

                // 업적의 보상 획득 상태를 true로 설정
                achievement.Acquire();

                // 업적을 리스트의 맨 뒤로 이동
                achievements.Remove(achievement);
                achievements.Add(achievement);

                // UI 갱신
                DisplayAchievements();

                // SP 텍스트 업데이트 및 데이터 저장
                UpdateSPText();
                SaveManager.instance.SaveData();
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
