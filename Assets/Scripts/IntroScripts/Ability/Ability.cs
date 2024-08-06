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
                Debug.Log("���� �̱���");
                break;
            case 6:
                Debug.Log("���� �̱���");
                break;
            default:
                Debug.LogWarning("�� �� ���� ��ų ��ȣ�Դϴ�: " + abilityNumber);
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
