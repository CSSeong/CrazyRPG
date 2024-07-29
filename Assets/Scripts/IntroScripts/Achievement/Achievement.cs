using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Achievements/Achievement")]
public class Achievement : ScriptableObject
{
    [Header("업적 이름")]
    public string achievementName;
    [Header("업적 내용")]
    public string description;
    [Header("업적 번호")]
    public float achievementNumber;
    [Header("획득 가능 SP")]
    public float achievementSP;

    private bool isUnlocked = false;

    // 업적을 잠금 해제하는 메소드
    public void Unlock()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            Debug.Log($"업적 달성: {achievementName}");
        }
    }
}

