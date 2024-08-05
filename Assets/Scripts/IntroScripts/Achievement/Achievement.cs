using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Achievements/Achievement")]
public class Achievement : ScriptableObject
{
    [Header("���� �̸�")]
    public string achievementName;
    [Header("���� ����")]
    public string description;
    [Header("���� ��ȣ")]
    public float achievementNumber;
    [Header("ȹ�� ���� SP")]
    public int achievementSP;
    [Header("Ŭ���� ����")]
    public bool isUnlocked = false;

    public void Unlock()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            Debug.Log($"���� �޼�: {achievementName}");
        }
    }

    public bool IsUnlocked()
    {
        return isUnlocked;
    }

    public void Reset()
    {
        isUnlocked = false;
    }
}

