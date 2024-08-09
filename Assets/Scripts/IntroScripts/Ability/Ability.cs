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
        get
        {
            return cooldown;
        }
        set
        {
            cooldown = value;
        }
    }
    [SerializeField]
    private bool isOnCooldown = false;
    public bool isAvailable_5 = false;

    public void Upgrade()
    {
        if (Level < maxlevel)
        {
            Level++;
            requiredSP++;
            ApplyEffect();
        }
    }

    public void ApplyEffect()
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
                isAvailable_5 = true;
                break;
            case 6:
                Debug.Log("아직 미구현");
                break;
            default:
                Debug.LogWarning("알 수 없는 스킬 번호입니다: " + abilityNumber);
                break;
        }
    }

    public IEnumerator ActivateAbility()
    {
        if (isOnCooldown || !isAvailable_5 || abilityNumber != 5)
        {
           Debug.Log("사용 불가");
            Debug.Log($"isOnCooldown: {isOnCooldown}, isAvailable_5: {isAvailable_5}, abilityNumber: {abilityNumber}");
            yield break;
        }

        Debug.Log("5번 스킬 사용");
        isOnCooldown = true;
        SaveManager.instance.UpdateAbilityCooldown(abilityNumber, true);

        // 이동 속도 증가
        float originalSpeed = SaveManager.instance.nowPlayer.moveSpeed;
        SaveManager.instance.UpdateMoveSpeed(originalSpeed + 3);

        // 5초 동안 유지
        yield return new WaitForSeconds(5f);

        // 원래 속도로 복원
        SaveManager.instance.UpdateMoveSpeed(originalSpeed);

        // 30초 쿨타임
        yield return new WaitForSeconds(cooldown);

        isOnCooldown = false;
        SaveManager.instance.UpdateAbilityCooldown(abilityNumber, false);
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
