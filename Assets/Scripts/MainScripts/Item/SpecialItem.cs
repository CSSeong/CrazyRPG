using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItem : ItemBase
{
    public float jumpBoost;          // 특수 속성: 점프 상승량
    public Vector3 teleportLocation; // 특수 속성: 텔레포트 위치

    public override void InitializeItem()
    {
        switch (itemType)
        {
            case ItemType.LightKit:
                itemName = "광원 설치 키트";
                description = "맵 바닥에 설치해 1분동안 광원을 유지합니다.";
                amount = 1;
                break;

            case ItemType.SpringShoes:
                itemName = "스프링 신발";
                description = "다음 점프의 점프력이 크게 상승합니다.";
                jumpBoost = 2.0f;
                amount = 1;
                break;

            case ItemType.TeleportScroll:
                itemName = "불완전한 TP 스크롤";
                description = "맵의 랜덤한 장소로 순간이동합니다.";
                amount = 1;
                teleportLocation = Vector3.zero;
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
                case ItemType.LightKit:
                    Debug.Log("광원 설치 키트 사용");
                    break;

                case ItemType.SpringShoes:
                    Debug.Log("스프링 신발 사용");
                    break;

                case ItemType.TeleportScroll:
                    Debug.Log("불완전한 TP 스크롤 사용");
                    // 실제 텔레포트 구현 필요
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


