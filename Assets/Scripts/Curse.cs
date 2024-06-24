using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurseType
{
    장작부식 = 1,
    안절부절,
    횟불고장1,
    횟불고장2,
    침침한눈,
    과유불급,
    돈벌어돈,
    망각,
    자원고갈,
    모아니면도
}

[System.Serializable]
public class Curse 
{
    public CurseType Type { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public GameObject maskObject;

    public Curse(CurseType type, string name, string description)
    {
        Type = type;
        Name = name;
        Description = description;
    }

    public void Apply(Player player)
    {
        switch (Type)
        {
            case CurseType.장작부식:
                ApplyCurse1(player);
                break;
            case CurseType.안절부절:
                ApplyCurse2(player);
                break;
            case CurseType.횟불고장1:
                ApplyCurse3(player);
                break;
            case CurseType.횟불고장2:
                ApplyCurse4(player);
                break;
            case CurseType.침침한눈:
                ApplyCurse5(player);
                break;
            case CurseType.과유불급:
                ApplyCurse6(player);
                break;
            case CurseType.돈벌어돈:
                ApplyCurse7(player);
                break;
            case CurseType.망각:
                ApplyCurse8(player);
                break;
            case CurseType.자원고갈:
                ApplyCurse9(player);
                break;
            case CurseType.모아니면도:
                ApplyCurse10(player);
                break;
        }
    }

    private void ApplyCurse1(Player player)
    {
        Debug.Log("장작부식 적용(아직 구현 못함");
        // 여기에 장작부식의 특정 동작을 구현
    }

    private void ApplyCurse2(Player player)
    {
        Debug.Log("안절부절 적용(구현 됨)");
        player.IsJumpDisabled = true;

    }

    private void ApplyCurse3(Player player)
    {
        Debug.Log("횟불고장1 적용(구현 됨)");
        player.PlayerLight.Lightreduction *= 2;
    }

    private void ApplyCurse4(Player player)
    {
        Debug.Log("횟불고장2 적용(구현 됨)");
        player.PlayerLight.MaxLightGage = 70;
    }

    private void ApplyCurse5(Player player)
    {
         Debug.Log("침침한눈 적용");
        player.PlayerLight.MaskImage.transform.localScale *= 0.5f;
    }

    private void ApplyCurse6(Player player)
    {
        Debug.Log("과유불급 적용");
        // 여기에 과유불급의 특정 동작을 구현
    }

    private void ApplyCurse7(Player player)
    {
        Debug.Log("돈벌어돈 적용");
        // 여기에 돈벌어돈의 특정 동작을 구현
    }

    private void ApplyCurse8(Player player)
    {
        Debug.Log("망각 적용");
        // 여기에 망각의 특정 동작을 구현
    }

    private void ApplyCurse9(Player player)
    {
        Debug.Log("자원고갈 적용");
        // 여기에 자원고갈의 특정 동작을 구현
    }

    private void ApplyCurse10(Player player)
    {
        Debug.Log("모아니면도 적용");
        // 여기에 모아니면도의 특정 동작을 구현
    }
}

