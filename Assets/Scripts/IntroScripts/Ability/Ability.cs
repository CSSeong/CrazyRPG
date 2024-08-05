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
    public float abilityNumber;
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
