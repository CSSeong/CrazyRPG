using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    [Header("�������� �� ������ ����Ʈ")]
    [SerializeField]
    private List<Item> mShopItems;

    [Header("���� ���� 3��")]
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
        // ���� ���� ������ŭ �ݺ��Ͽ� ������ ����
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
            // 0������ 3�� ������ ������ �� �������� ����
            int randomIndex = Random.Range(0, 4);
            selectedItem = mShopItems[randomIndex];
        }
        else
        {
            // 4������ 7�� ������ ������ �� �������� ����
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
            Debug.Log("�������� �����߽��ϴ�: " + item.ItemName);
            UpdateShopUI();
        }
        else
        {
            Debug.Log("������ �����մϴ�.");
        }
    }
}
