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
                itemName = "고대 폭파 장치";
                description = "어디선가 써먹을 곳이 있을 것만 같다.";
                amount = 1;
                break;

            case ItemType.MagitechTerminal:
                itemName = "마법 공학 단말기";
                description = "던전 바깥 구조대에 구조 요청을 할 수 있는 단말기, 배터리를 사용해 충전해야 한다.";
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
                case ItemType.AncientExplosiveDevice:
                    Debug.Log("고대 폭파 장치 사용");
                    break;

                case ItemType.MagitechTerminal:
                    Debug.Log("마법 공학 단말기 사용");
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


