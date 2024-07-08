using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using TMPro;

public class ItemSlot
{
    public ItemBase Item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uidSlot;
    public ItemSlot[] slots;

    public GameObject inventoryWindow;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public GameObject useButton;
    public GameObject registerButton;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[uidSlot.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            // UI Slot 초기화하기
            slots[i] = new ItemSlot();
            uidSlot[i].index = i;
            uidSlot[i].Clear();
        }
        ClearSelectItemWindow();
    }


    public void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)  // 현재 하이라키에 inventoryWindow가 켜져있는 지 확인
        {
            inventoryWindow.SetActive(false);       // 현재 하이라키에 inventoryWindow가 켜져있다면 끄기
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(ItemBase item)
    {
        if (item.canStack)  // 아이템이 쌓일 수 있는 아이템인지 확인
        {
            // 쌓을 수 있는 아이템일 경우 스택을 쌓아준다.
            ItemSlot slotToStackTo = GetItemStack(item);
            if (slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        // 없을 경우 빈칸에 아이템을 추가해준다.
        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }

    }

    void UpdateUI()
    {
        // slots에 있는 아이템 데이터로 UI의 Slot 최신화하기
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
                uidSlot[i].Set(slots[i]);
            else
                uidSlot[i].Clear();
        }
    }

    ItemSlot GetItemStack(ItemBase item)
    {
        // 현재 선택된 아이템이 이미 슬롯에 있고, 아직 최대수량을 안 넘겼다면 해당 아이템이 위치한 슬롯의 위치를 가져온다.
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
                return slots[i];
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        // 빈 슬롯 찾기
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }

    public void SelectItem(int index)
    {
        // 선택한 슬롯에 아이템이 없을 경우 return
        if (slots[index].item == null) return;

        // 선택한 아이템 정보 가져오기
        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;
    }

    private void ClearSelectItemWindow()
    {
        // 아이템 초기화
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        useButton.SetActive(false);
        registerButton.SetActive(false);
    }
    public void OnUseButton()
    {
        //사용 함수 추가

        // 사용한 아이템 없애기
        RemoveSelectedItem();
    }

    private void RemoveSelectedItem()
    {
        selectedItem.quantity--;    // 수량 깎기.

        // 아이템의 남은 수량이 0이 되면
        if (selectedItem.quantity <= 0)
        {
            // 아이템 제거 및 UI에서도 아이템 정보 지우기
            selectedItem.item = null;
            ClearSelectItemWindow();
        }
        UpdateUI();
    }

    public bool HasItems(ItemBase item, int quantity)
    {
        return false;
    }
}
