using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlessingType
{
    ��Ȱ�ǰ�ȣ = 1,
    ȭ�º���,
    �˳��Ѷ���,
    ���Ǵ�,
    �����,
    ����,
    ȭ�º���,
    ��ƴϸ鵵,
    ����ô��,
    ��ġ��ü��
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
            case BlessingType.��Ȱ�ǰ�ȣ:
                ApplyBlessing1(player);
                break;
            case BlessingType.ȭ�º���:
                ApplyBlessing2(player);
                break;
            case BlessingType.�˳��Ѷ���:
                ApplyBlessing3(player);
                break;
            case BlessingType.���Ǵ�:
                ApplyBlessing4(player);
                break;
            case BlessingType.�����:
                ApplyBlessing5(player);
                break;
            case BlessingType.����:
                ApplyBlessing6(player);
                break;
            case BlessingType.ȭ�º���:
                ApplyBlessing7(player);
                break;
            case BlessingType.��ƴϸ鵵:
                ApplyBlessing8(player);
                break;
            case BlessingType.����ô��:
                ApplyBlessing9(player);
                break;
            case BlessingType.��ġ��ü��:
                ApplyBlessing10(player);
                break;
        }
    }

    private void ApplyBlessing1(Player player)
    {
        Debug.Log("��Ȱ�ǰ�ȣ ����(���� ��)");
        player.IsBlessing = true;
    }

    private void ApplyBlessing2(Player player)
    {
        Debug.Log("ȭ�º��� ����(���� ��)");
        player.PlayerLight.Lightreduction *= 1.15f;
    }

    private void ApplyBlessing3(Player player)
    {
        Debug.Log("�˳��Ѷ��� ����(���� ��)");
        player.PlayerLight.MaxLightGage *= 1.2f;
    }

    private void ApplyBlessing4(Player player)
    {
        Debug.Log("���Ǵ� ����(���� ��)");
        player.PlayerLight.MaskImage.transform.localScale *= 1.25f;
    }

    private void ApplyBlessing5(Player player)
    {
         Debug.Log("����� ����(���� �ȵ�)");    
    }

    private void ApplyBlessing6(Player player)
    {
        Debug.Log("���� ����(���� �ȵ�)");      
    }

    private void ApplyBlessing7(Player player)
    {
        Debug.Log("ȭ�º��� ����(���� �� ��");        
    }

    private void ApplyBlessing8(Player player)
    {
        Debug.Log("��ƴϸ鵵 ����(���� �ȵ�");        
    }

    private void ApplyBlessing9(Player player)
    {
        Debug.Log("����ô�� ����(���� �ȵ�");        
    }

    private void ApplyBlessing10(Player player)
    {
        Debug.Log("��ġ��ü�� ����(���� ��)");
        player.PlayerHP.MaxHP *= 1.3f;
    }
}

