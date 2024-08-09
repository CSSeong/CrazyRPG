using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Abilities/Ability")]
public class Ability : ScriptableObject
{
    [Header("��ų �̸�")]
    public string abilityName;
    [Header("��ų ����")]
    public string description;
    [Header("��ų ��ȣ")]
    public int abilityNumber;
    [Header("�䱸 SP")]
    public int requiredSP;
    [Header("�ִ� ��ȭ ����")]
    public int maxlevel;
    [Header("��ų ������")]
    public Sprite icon;
    [Header("����")]
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
                Debug.Log("���� �̱���");
                break;
            default:
                Debug.LogWarning("�� �� ���� ��ų ��ȣ�Դϴ�: " + abilityNumber);
                break;
        }
    }

    public IEnumerator ActivateAbility()
    {
        if (isOnCooldown || !isAvailable_5 || abilityNumber != 5)
        {
           Debug.Log("��� �Ұ�");
            Debug.Log($"isOnCooldown: {isOnCooldown}, isAvailable_5: {isAvailable_5}, abilityNumber: {abilityNumber}");
            yield break;
        }

        Debug.Log("5�� ��ų ���");
        isOnCooldown = true;
        SaveManager.instance.UpdateAbilityCooldown(abilityNumber, true);

        // �̵� �ӵ� ����
        float originalSpeed = SaveManager.instance.nowPlayer.moveSpeed;
        SaveManager.instance.UpdateMoveSpeed(originalSpeed + 3);

        // 5�� ���� ����
        yield return new WaitForSeconds(5f);

        // ���� �ӵ��� ����
        SaveManager.instance.UpdateMoveSpeed(originalSpeed);

        // 30�� ��Ÿ��
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
