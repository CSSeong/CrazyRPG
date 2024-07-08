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
                itemName = "장작";
                description = "최대 횟불 게이지의 50%만큼 충전한다.";
                amount = 0;
                break;

            case ItemType.Battery:
                itemName = "배터리";
                description = "기계장치를 작동 시킬 수 있다.";
                amount = 0;
                break;

            case ItemType.HealingPotion:
                itemName = "회복약";
                description = "최대 HP의 100%만큼 회복한다.";
                amount = 0;
                break;

            case ItemType.Key:
                itemName = "열쇠";
                description = "특별한 보물상자의 잠금을 해제한다.";
                isUsed = false;
                amount = 1;
                break;

            default:
                itemName = "알 수 없는 아이템";
                description = "설명이 없습니다.";
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
                    Debug.Log("장작 사용");
                    break;

                case ItemType.Battery:
                    Debug.Log("배터리 사용");
                    break;

                case ItemType.HealingPotion:
                    Debug.Log("회복약 사용");
                    break;

                case ItemType.Key:
                    Debug.Log("열쇠 사용");
                    isUsed = true;
                    break;

                default:
                    Debug.Log("알 수 없는 아이템 타입");
                    break;
            }
        }
        else
        {
            Debug.Log("아이템의 수량이 부족합니다.");
        }
    }
}


