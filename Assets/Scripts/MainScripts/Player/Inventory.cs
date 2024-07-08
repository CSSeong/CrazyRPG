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
            // UI Slot �ʱ�ȭ�ϱ�
            slots[i] = new ItemSlot();
            uidSlot[i].index = i;
            uidSlot[i].Clear();
        }
        ClearSelectItemWindow();
    }


    public void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)  // ���� ���̶�Ű�� inventoryWindow�� �����ִ� �� Ȯ��
        {
            inventoryWindow.SetActive(false);       // ���� ���̶�Ű�� inventoryWindow�� �����ִٸ� ����
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
        if (item.canStack)  // �������� ���� �� �ִ� ���������� Ȯ��
        {
            // ���� �� �ִ� �������� ��� ������ �׾��ش�.
            ItemSlot slotToStackTo = GetItemStack(item);
            if (slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        // ���� ��� ��ĭ�� �������� �߰����ش�.
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
        // slots�� �ִ� ������ �����ͷ� UI�� Slot �ֽ�ȭ�ϱ�
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
        // ���� ���õ� �������� �̹� ���Կ� �ְ�, ���� �ִ������ �� �Ѱ�ٸ� �ش� �������� ��ġ�� ������ ��ġ�� �����´�.
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
                return slots[i];
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        // �� ���� ã��
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }

    public void SelectItem(int index)
    {
        // ������ ���Կ� �������� ���� ��� return
        if (slots[index].item == null) return;

        // ������ ������ ���� ��������
        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;
    }

    private void ClearSelectItemWindow()
    {
        // ������ �ʱ�ȭ
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        useButton.SetActive(false);
        registerButton.SetActive(false);
    }
    public void OnUseButton()
    {
        //��� �Լ� �߰�

        // ����� ������ ���ֱ�
        RemoveSelectedItem();
    }

    private void RemoveSelectedItem()
    {
        selectedItem.quantity--;    // ���� ���.

        // �������� ���� ������ 0�� �Ǹ�
        if (selectedItem.quantity <= 0)
        {
            // ������ ���� �� UI������ ������ ���� �����
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
