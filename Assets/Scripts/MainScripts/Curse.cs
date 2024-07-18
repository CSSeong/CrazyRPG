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
    돈이좋아,
    망각,
    자원고갈,
    상자의저주
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

    //저주 적용 로직
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
            case CurseType.돈이좋아:
                ApplyCurse7(player);
                break;
            case CurseType.망각:
                ApplyCurse8(player);
                break;
            case CurseType.자원고갈:
                ApplyCurse9(player);
                break;
            case CurseType.상자의저주:
                ApplyCurse10(player);
                break;
        }
    }

    private void ApplyCurse1(Player player)
    {
        Debug.Log("장작부식 적용(구현 됨)");
        player.PlayerLight.Availability = false;
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
        player.PlayerLight.MaxLightGage *= 0.7f;
    }

    private void ApplyCurse5(Player player)
    {
         Debug.Log("침침한눈 적용(구현 됨)");
        player.PlayerLight.MaskImage.transform.localScale *= 0.5f;
    }

    private void ApplyCurse6(Player player)
    {
        Debug.Log("과유불급 적용(구현 됨)");
        player.IsRunFastEnabled = true;
    }

    private void ApplyCurse7(Player player)
    {
        Debug.Log("돈이좋아 적용(구현 안 됨");
        // 여기에 돈벌어돈의 특정 동작을 구현
    }

    private void ApplyCurse8(Player player)
    {
        Debug.Log("망각 적용(구현 됨");
        player.LightUI.SetActive(false);
        
    }

    private void ApplyCurse9(Player player)
    {
        Debug.Log("자원고갈 적용");
        // 여기에 자원고갈의 특정 동작을 구현
    }

    private void ApplyCurse10(Player player)
    {
        Debug.Log("상자의 저주 적용");
        // 여기에 모아니면도의 특정 동작을 구현
    }

    // 저주 해제 로직
    public void Remove(Player player)
    {
        switch (Type)
        {
            case CurseType.장작부식:
                RemoveCurse1(player);
                break;
            case CurseType.안절부절:
                RemoveCurse2(player);
                break;
            case CurseType.횟불고장1:
                RemoveCurse3(player);
                break;
            case CurseType.횟불고장2:
                RemoveCurse4(player);
                break;
            case CurseType.침침한눈:
                RemoveCurse5(player);
                break;
            case CurseType.과유불급:
                RemoveCurse6(player);
                break;
            case CurseType.돈이좋아:
                RemoveCurse7(player);
                break;
            case CurseType.망각:
                RemoveCurse8(player);
                break;
            case CurseType.자원고갈:
                RemoveCurse9(player);
                break;
            case CurseType.상자의저주:
                RemoveCurse10(player);
                break;
            default:
                Debug.Log("저주가 없습니다.");
                break;
        }
    }

    private void RemoveCurse1(Player player)
    {
        Debug.Log("장작부식 제거됨");
        player.PlayerLight.Availability = true;
    }

    private void RemoveCurse2(Player player)
    {
        Debug.Log("안절부절 제거됨");
        player.IsJumpDisabled = false;
    }

    private void RemoveCurse3(Player player)
    {
        Debug.Log("횟불고장1 제거됨");
        player.PlayerLight.Lightreduction /= 2;
    }

    private void RemoveCurse4(Player player)
    {
        Debug.Log("횟불고장2 제거됨");
        player.PlayerLight.MaxLightGage /= 0.7f;
    }

    private void RemoveCurse5(Player player)
    {
        Debug.Log("침침한눈 제거됨");
        player.PlayerLight.MaskImage.transform.localScale /= 0.5f;
    }

    private void RemoveCurse6(Player player)
    {
        Debug.Log("과유불급 제거됨");
        player.IsRunFastEnabled = false;
    }

    private void RemoveCurse7(Player player)
    {
        Debug.Log("돈이좋아 제거됨(구현 필요)");
        // 구현 필요
    }

    private void RemoveCurse8(Player player)
    {
        Debug.Log("망각 제거됨");
        player.LightUI.SetActive(true);
    }

    private void RemoveCurse9(Player player)
    {
        Debug.Log("자원고갈 제거됨(구현 필요)");
        // 구현 필요
    }

    private void RemoveCurse10(Player player)
    {
        Debug.Log("상자의저주 제거됨(구현 필요)");
        // 구현 필요
    }

}



