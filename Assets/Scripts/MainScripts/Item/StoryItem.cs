using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryItem : ItemBase
{
    public override void InitializeItem()
    {
        switch (itemType)
        {
            case ItemType.AncientExplosiveDevice:
                itemName = "��� ���� ��ġ";
                description = "��𼱰� ����� ���� ���� �͸� ����.";
                amount = 1;
                break;

            case ItemType.MagitechTerminal:
                itemName = "���� ���� �ܸ���";
                description = "���� �ٱ� �����뿡 ���� ��û�� �� �� �ִ� �ܸ���, ���͸��� ����� �����ؾ� �Ѵ�.";
                amount = 1;
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
                case ItemType.AncientExplosiveDevice:
                    Debug.Log("��� ���� ��ġ ���");
                    break;

                case ItemType.MagitechTerminal:
                    Debug.Log("���� ���� �ܸ��� ���");
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


