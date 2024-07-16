using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    [Header("상점 UI")]
    [SerializeField]
    private GameObject shopUI;

    [Header("상점에서 팔 아이템 리스트")]
    [SerializeField]
    private List<Item> mShopItems;

    [Header("상점 슬롯 3개")]
    [SerializeField]
    private List<ShopItemUI> itemslots;

    [Header("버튼 UI")]
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
                Debug.Log("아이템을 구매했습니다: " + itemToBuy.ItemName);

                // 구매 후 선택된 슬롯을 비웁니다.
                selectedSlot.ClearSlot();
            }
            else
            {
                Debug.Log("코인이 부족합니다.");
            }
        }
        else
        {
            Debug.Log("아이템을 먼저 선택해주세요.");
        }
    }

    public void CloseShopUI()
    {
        shopUI.SetActive(false);
    }

    // 상점 UI를 활성화하는 메서드
    public void OpenShopUI()
    {
        shopUI.SetActive(true);
    }
}
