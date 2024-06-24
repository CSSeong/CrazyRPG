using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurseType
{
    ���ۺν� = 1,
    ��������,
    Ƚ�Ұ���1,
    Ƚ�Ұ���2,
    ħħ�Ѵ�,
    �����ұ�,
    �����,
    ����,
    �ڿ���,
    ��ƴϸ鵵
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
            case CurseType.���ۺν�:
                ApplyCurse1(player);
                break;
            case CurseType.��������:
                ApplyCurse2(player);
                break;
            case CurseType.Ƚ�Ұ���1:
                ApplyCurse3(player);
                break;
            case CurseType.Ƚ�Ұ���2:
                ApplyCurse4(player);
                break;
            case CurseType.ħħ�Ѵ�:
                ApplyCurse5(player);
                break;
            case CurseType.�����ұ�:
                ApplyCurse6(player);
                break;
            case CurseType.�����:
                ApplyCurse7(player);
                break;
            case CurseType.����:
                ApplyCurse8(player);
                break;
            case CurseType.�ڿ���:
                ApplyCurse9(player);
                break;
            case CurseType.��ƴϸ鵵:
                ApplyCurse10(player);
                break;
        }
    }

    private void ApplyCurse1(Player player)
    {
        Debug.Log("���ۺν� ����(���� ���� ����");
        // ���⿡ ���ۺν��� Ư�� ������ ����
    }

    private void ApplyCurse2(Player player)
    {
        Debug.Log("�������� ����(���� ��)");
        player.IsJumpDisabled = true;

    }

    private void ApplyCurse3(Player player)
    {
        Debug.Log("Ƚ�Ұ���1 ����(���� ��)");
        player.PlayerLight.Lightreduction *= 2;
    }

    private void ApplyCurse4(Player player)
    {
        Debug.Log("Ƚ�Ұ���2 ����(���� ��)");
        player.PlayerLight.MaxLightGage = 70;
    }

    private void ApplyCurse5(Player player)
    {
         Debug.Log("ħħ�Ѵ� ����");
        player.PlayerLight.MaskImage.transform.localScale *= 0.5f;
    }

    private void ApplyCurse6(Player player)
    {
        Debug.Log("�����ұ� ����");
        // ���⿡ �����ұ��� Ư�� ������ ����
    }

    private void ApplyCurse7(Player player)
    {
        Debug.Log("����� ����");
        // ���⿡ ������� Ư�� ������ ����
    }

    private void ApplyCurse8(Player player)
    {
        Debug.Log("���� ����");
        // ���⿡ ������ Ư�� ������ ����
    }

    private void ApplyCurse9(Player player)
    {
        Debug.Log("�ڿ��� ����");
        // ���⿡ �ڿ����� Ư�� ������ ����
    }

    private void ApplyCurse10(Player player)
    {
        Debug.Log("��ƴϸ鵵 ����");
        // ���⿡ ��ƴϸ鵵�� Ư�� ������ ����
    }
}

