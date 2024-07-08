using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralItem : ItemBase
{
    public override void InitializeItem()
    {
        switch (itemType)
        {
            case ItemType.Firewood:
                itemName = "����";
                description = "�ִ� Ƚ�� �������� 50%��ŭ �����Ѵ�.";
                amount = 0;
                break;

            case ItemType.Battery:
                itemName = "���͸�";
                description = "�����ġ�� �۵� ��ų �� �ִ�.";
                amount = 0;
                break;

            case ItemType.HealingPotion:
                itemName = "ȸ����";
                description = "�ִ� HP�� 100%��ŭ ȸ���Ѵ�.";
                amount = 0;
                break;

            case ItemType.Key:
                itemName = "����";
                description = "Ư���� ���������� ����� �����Ѵ�.";
                isUsed = false;
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
                case ItemType.Firewood:
                    Debug.Log("���� ���");
                    break;

                case ItemType.Battery:
                    Debug.Log("���͸� ���");
                    break;

                case ItemType.HealingPotion:
                    Debug.Log("ȸ���� ���");
                    break;

                case ItemType.Key:
                    Debug.Log("���� ���");
                    isUsed = true;
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


