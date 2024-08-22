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
    public int achievementSP;
    [Header("클리어 여부")]
    public bool isUnlocked = false;
    [Header("보상 획득 여부")]
    public bool isAcquire = false;

    public void Unlock()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            Debug.Log($"업적 달성: {achievementName}");
        }
    }

    public bool IsUnlocked()
    {
        return isUnlocked;
    }

    public void Acquire()
    {
        if (isUnlocked && !isAcquire)
        {
            isAcquire = true;
            Debug.Log($"보상 획득: {achievementName}");
        }
    }



    public void Reset()
    {
        isUnlocked = false;
    }
}

