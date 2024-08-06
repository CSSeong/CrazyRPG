using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlessingType
{
    부활의가호 = 1,
    화력보존,
    넉넉한뗄감,
    매의눈,
    돈벌어돈,
    해주,
    화력보급,
    모아니면도,
    저주척결,
    넘치는체력
}

[System.Serializable]
public class Blessing 
{
    public BlessingType Type { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public GameObject maskObject;


    public Blessing(BlessingType type, string name, string description)
    {
        Type = type;
        Name = name;
        Description = description;
    }



    public void Apply(Player player)
    {
        switch (Type)
        {
            case BlessingType.부활의가호:
                ApplyBlessing1(player);
                break;
            case BlessingType.화력보존:
                ApplyBlessing2(player);
                break;
            case BlessingType.넉넉한뗄감:
                ApplyBlessing3(player);
                break;
            case BlessingType.매의눈:
                ApplyBlessing4(player);
                break;
            case BlessingType.돈벌어돈:
                ApplyBlessing5(player);
                break;
            case BlessingType.해주:
                ApplyBlessing6(player);
                break;
            case BlessingType.화력보급:
                ApplyBlessing7(player);
                break;
            case BlessingType.모아니면도:
                ApplyBlessing8(player);
                break;
            case BlessingType.저주척결:
                ApplyBlessing9(player);
                break;
            case BlessingType.넘치는체력:
                ApplyBlessing10(player);
                break;
        }
    }

    private void ApplyBlessing1(Player player)
    {
        Debug.Log("부활의가호 적용(구현 됨)");
        player.IsBlessing = true;
    }

    private void ApplyBlessing2(Player player)
    {
        Debug.Log("화력보존 적용(구현 됨)");
        player.PlayerLight.Lightreduction *= 1.15f;
    }

    private void ApplyBlessing3(Player player)
    {
        Debug.Log("넉넉한뗄감 적용(구현 됨)");
        player.PlayerLight.MaxLightGage *= 1.2f;
    }

    private void ApplyBlessing4(Player player)
    {
        Debug.Log("매의눈 적용(구현 됨)");
        player.PlayerLight.MaskImage.transform.localScale *= 1.25f;
    }

    private void ApplyBlessing5(Player player)
    {
         Debug.Log("돈벌어돈 적용(구현 안됨)");    
    }

    private void ApplyBlessing6(Player player)
    {
        Debug.Log("해주 적용(구현 됨)");
        if (player.activeCurses.Count == 0)
        {
           Debug.Log("해주할 저주가 없습니다");
        }
        else
        {
            System.Random random = new System.Random();
            int randomIndex = random.Next(player.activeCurses.Count);
            Curse curseToRemove = player.activeCurses[randomIndex];
            curseToRemove.Remove(player);
        }
    }

    private void ApplyBlessing7(Player player)
    {
        Debug.Log("화력보급 적용(구현 안 됨");        
    }

    private void ApplyBlessing8(Player player)
    {
        Debug.Log("모아니면도 적용(구현 됨)");
        System.Random random = new System.Random();

        if (BlessingManager.instance.CurseSelection.Curses.Count > 0)
        {
            int randomIndex = random.Next(BlessingManager.instance.CurseSelection.Curses.Count);
            Curse randomCurse = BlessingManager.instance.CurseSelection.Curses[randomIndex];
            randomCurse.Apply(player);
            BlessingManager.instance.CurseSelection.Curses.RemoveAt(randomIndex);
        }

        for (int i = 0; i < 2; i++)
        {
            if (BlessingManager.instance.BlessingSelection._Blessings.Count == 0)
                break;

            int randomIndex = random.Next(BlessingManager.instance.BlessingSelection._Blessings.Count);
            Blessing randomBlessing = BlessingManager.instance.BlessingSelection._Blessings[randomIndex];
            randomBlessing.Apply(player);
            BlessingManager.instance.BlessingSelection._Blessings.RemoveAt(randomIndex);

            if (randomBlessing.Name == "부활의가호")
            {
                BlessingManager.instance.BlessingSelection._Blessings.RemoveAll(blessing => blessing.Name == "부활의가호");
            }
        }
    }

    private void ApplyBlessing9(Player player)
    {
        Debug.Log("저주척결 적용(구현 안됨");
    }

    private void ApplyBlessing10(Player player)
    {
        Debug.Log("넘치는체력 적용(구현 됨)");
        player.PlayerHP.MaxHP *= 1.3f;
        SaveManager.instance.nowPlayer.playerHP_max = player.PlayerHP.MaxHP;
    }
}

