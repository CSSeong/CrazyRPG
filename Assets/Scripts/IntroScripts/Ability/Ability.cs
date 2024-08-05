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
    public float abilityNumber;
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
