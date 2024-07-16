using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    [Header("���� UI")]
    [SerializeField]
    private GameObject shopUI;

    [Header("�������� �� ������ ����Ʈ")]
    [SerializeField]
    private List<Item> mShopItems;

    [Header("���� ���� 3��")]
    [SerializeField]
    private List<ShopItemUI> itemslots;

    [Header("��ư UI")]
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private Button stillButton;
    [SerializeField]
    private Button exitShop;
    [SerializeField]
    private Button openShop;

    private PlayerData playerData;
    private ShopItemUI selectedSlot;

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
        buyButton.onClick.AddListener(BuySelectedSlotItem);
        openShop.onClick.AddListener(OpenShopUI);
        exitShop.onClick.AddListener(CloseShopUI);

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

    public void SelectSlot(ShopItemUI slot)
    {
        selectedSlot = slot;
    }

    public void BuySelectedSlotItem()
    {
        if (selectedSlot != null)
        {
            Item itemToBuy = selectedSlot.GetItem();
            if (playerData.Coin >= itemToBuy.Price)
            {
                playerData.Coin -= itemToBuy.Price;
                InventoryMain.Instance.AcquireItem(itemToBuy);
                Debug.Log("�������� �����߽��ϴ�: " + itemToBuy.ItemName);

                // ���� �� ���õ� ������ ���ϴ�.
                selectedSlot.ClearSlot();
            }
            else
            {
                Debug.Log("������ �����մϴ�.");
            }
        }
        else
        {
            Debug.Log("�������� ���� �������ּ���.");
        }
    }

    public void CloseShopUI()
    {
        shopUI.SetActive(false);
    }

    // ���� UI�� Ȱ��ȭ�ϴ� �޼���
    public void OpenShopUI()
    {
        shopUI.SetActive(true);
    }
}
