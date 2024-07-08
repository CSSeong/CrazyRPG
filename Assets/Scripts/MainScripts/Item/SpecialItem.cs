using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItem : ItemBase
{
    public float jumpBoost;          // Ư�� �Ӽ�: ���� ��·�
    public Vector3 teleportLocation; // Ư�� �Ӽ�: �ڷ���Ʈ ��ġ

    public override void InitializeItem()
    {
        switch (itemType)
        {
            case ItemType.LightKit:
                itemName = "���� ��ġ ŰƮ";
                description = "�� �ٴڿ� ��ġ�� 1�е��� ������ �����մϴ�.";
                amount = 1;
                break;

            case ItemType.SpringShoes:
                itemName = "������ �Ź�";
                description = "���� ������ �������� ũ�� ����մϴ�.";
                jumpBoost = 2.0f;
                amount = 1;
                break;

            case ItemType.TeleportScroll:
                itemName = "�ҿ����� TP ��ũ��";
                description = "���� ������ ��ҷ� �����̵��մϴ�.";
                amount = 1;
                teleportLocation = Vector3.zero;
                break;

            default:
                itemName = "�� �� ���� ������";
                description = "������ �����ϴ�.";
                break;
        }
    }

    public override void UseItem()
    {
        if (amount > 0)
        {
            amount--;
            switch (itemType)
            {
                case ItemType.LightKit:
                    Debug.Log("���� ��ġ ŰƮ ���");
                    break;

                case ItemType.SpringShoes:
                    Debug.Log("������ �Ź� ���");
                    break;

                case ItemType.TeleportScroll:
                    Debug.Log("�ҿ����� TP ��ũ�� ���");
                    // ���� �ڷ���Ʈ ���� �ʿ�
                    break;

                default:
                    Debug.Log("�� �� ���� ������ Ÿ��");
                    break;
            }
        }
        else
        {
            Debug.Log("�������� ������ �����մϴ�.");
        }
    }
}


