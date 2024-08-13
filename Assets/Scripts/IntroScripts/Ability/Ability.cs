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

    private float cooldown = 5f;
    public float Cooldown
    {
        get => cooldown;
        set => cooldown = value;
    }

    public bool isOnCooldown;
    public bool isUnlocked = false;  // 능력 개방 여부를 나타내는 속성

    public void Upgrade()
    {
        if (Level < maxlevel)
        {
            Level++;
            requiredSP++;
            ApplyEffect();
        }
    }
    // 능력을 개방 하면 isUnlocked이 true가 되도록 해볼 것.
    public void ApplyEffect()
    {
        if (SaveManager.instance == null) return;

        if (!isUnlocked) return;  // 능력이 개방되지 않은 경우 효과를 적용하지 않음

        switch (abilityNumber)
        {
            case 1:
                switch (Level)
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
                // 5번 능력은 ApplyEffect에서 isAvailable을 설정하지 않습니다.
                break;
            case 6:
                Debug.Log("아직 미구현");
                break;
            default:
                Debug.LogWarning("알 수 없는 스킬 번호입니다: " + abilityNumber);
                break;
        }
    }

    public bool CanUpgrade() => Level < maxlevel;

    public bool IsMaxLevel() => Level >= maxlevel;

    public void Reset() => Level = 0;
}