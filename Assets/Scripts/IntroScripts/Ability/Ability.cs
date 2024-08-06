using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Abilities/Ability")]
public class Ability : ScriptableObject
{
    [Header("스킬 이름")]
    public string abilityName;
    [Header("스킬 내용")]
    public string description;
    [Header("스킬 번호")]
    public int abilityNumber;
    [Header("요구 SP")]
    public int requiredSP;
    [Header("최대 강화 레벨")]
    public int maxlevel;
    [Header("스킬 아이콘")]
    public Sprite icon;
    [Header("레벨")]
    public int Level = 0;

    public void Upgrade()
    {
        if (Level < maxlevel)
        {
            Level++;
            requiredSP++;
            ApplyEffect();
        }
    }

    private void ApplyEffect()
    {
        if (SaveManager.instance == null) return;

        switch (abilityNumber)
        {
            case 1:
                switch(Level)
                {
                    case 1:
                        SaveManager.instance.UpdateMoveSpeed(4.6f);
                        break;
                    case 2:
                        SaveManager.instance.UpdateMoveSpeed(4.7f);
                        break;
                    case 3:
                        SaveManager.instance.UpdateMoveSpeed(4.8f);
                        break;
                    case 4:
                        SaveManager.instance.UpdateMoveSpeed(5);
                        break;
                }
                break;
            case 2:
                switch (Level)
                {
                    case 1:
                        SaveManager.instance.UpdateJumpForce(10.2f);
                        break;
                    case 2:
                        SaveManager.instance.UpdateJumpForce(10.4f);
                        break;
                    case 3:
                        SaveManager.instance.UpdateJumpForce(10.7f);
                        break;
                    case 4:
                        SaveManager.instance.UpdateJumpForce(11);
                        break;
                }
                break;
            case 3:
                SaveManager.instance.UpdatePlayerLightGage(20 * Level);
                break;
            case 4:
                SaveManager.instance.UpdatePlayerHP(20 * Level);
                break;
            case 5:
                Debug.Log("아직 미구현");
                break;
            case 6:
                Debug.Log("아직 미구현");
                break;
            default:
                Debug.LogWarning("알 수 없는 스킬 번호입니다: " + abilityNumber);
                break;
        }
    }

    public bool CanUpgrade()
    {
        return Level < maxlevel;
    }

    public bool IsMaxLevel()
    {
        return Level >= maxlevel;
    }

    public void Reset()
    {
        Level = 0;
    }
}
