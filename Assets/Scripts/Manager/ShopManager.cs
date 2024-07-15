using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    [Header("상점에서 팔 아이템 리스트")]
    [SerializeField]
    private List<Item> mShopItems;

    [Header("상점 슬롯 3개")]
    [SerializeField]
    private List<ShopItemUI> itemslots;

    private PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        UpdateShopUI();
    }


    private void UpdateShopUI()
    {
        // 상점 슬롯 개수만큼 반복하여 아이템 설정
        for (int i = 0; i < itemslots.Count; i++)
        {
            Item randomItem = GetRandomItem(i);
            itemslots[i].Setup(randomItem, this);
        }
    }

    private Item GetRandomItem(int slotIndex)
    {
        Item selectedItem = null;

        if (slotIndex < 2)
        {
            // 0번에서 3번 사이의 아이템 중 랜덤으로 선택
            int randomIndex = Random.Range(0, 4);
            selectedItem = mShopItems[randomIndex];
        }
        else
        {
            // 4번에서 7번 사이의 아이템 중 랜덤으로 선택
            int randomIndex = Random.Range(4, 8);
            selectedItem = mShopItems[randomIndex];
        }

        return selectedItem;
    }

    public void BuyItem(Item item)
    {
        if(playerData.Coin >= item.Price)
        {
            playerData.Coin -= item.Price;
            InventoryMain.Instance.AcquireItem(item);
            Debug.Log("아이템을 구매했습니다: " + item.ItemName);
            UpdateShopUI();
        }
        else
        {
            Debug.Log("코인이 부족합니다.");
        }
    }
}
