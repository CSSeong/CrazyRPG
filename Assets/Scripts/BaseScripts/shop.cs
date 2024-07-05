using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();
    private PlayerData player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
    }

    public void BuyItem(Item item)
    {
        if(player.Coin >= item.itemCost)
        {
            player.Coin -= item.itemCost;
            Debug.Log("아이템을 구매했습니다: " + item.itemName);
            //아이템 추가 코드
        }
        else
        {
            Debug.Log("골드가 부족합니다!");
        }
    }

    public void StealItem(Item item)
    {
        Debug.Log("아이템을 훔쳤습니다: " + item.itemName);
        // 아이템 추가 코드
        RemoveItemFromShop(item); // 상점에서 아이템 제거
    }

    public void InitializeShop()
    {
        // 예시 아이템 추가 (아이템은 실제 게임에서 다른 방식으로 추가될 수 있음)
    }

    private void RemoveItemFromShop(Item item)
    {
        itemList.Remove(item);
    }
}
