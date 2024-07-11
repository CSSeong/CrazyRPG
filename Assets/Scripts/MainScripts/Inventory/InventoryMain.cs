using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMain : InventoryBase
{
    public static InventoryMain Instance { get; private set; }

    public static bool IsInventoryActive = false;

    [Header("������ ���� ǥ�� UI")]
    [SerializeField]
    private TextMeshProUGUI itemNameText;
    [SerializeField]
    private TextMeshProUGUI itemDescriptionText;

    private List<InventorySlotData> inventoryData = new List<InventorySlotData>();

    new void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ������ �κ��丮 ������Ʈ�� �� ��ȯ �� �������� �ʵ��� �մϴ�.
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        base.Awake();
    }

    private void Start()
    {
        // �� ���Կ� Ŭ�� �̺�Ʈ�� ����մϴ�.
        for (int i = 0; i < mSlots.Length; i++)
        {
            int slotIndex = i; // ���� �ε����� Ŭ������ �������� ���� ����մϴ�.
            mSlots[i].GetComponent<Button>().onClick.AddListener(() => OnSlotClicked(slotIndex));
        }
    }

    private void OnSlotClicked(int slotIndex)
    {
        InventorySlot clickedSlot = mSlots[slotIndex];

        // Ŭ���� ���Կ� �������� ���� ��� �ش� ������ UI�� ǥ���մϴ�.
        if (clickedSlot.Item != null)
        {
            itemNameText.text = clickedSlot.Item.ItemName;
            itemDescriptionText.text = clickedSlot.Item.ItemDescription;
        }
        else
        {
            // ���Կ� �������� ���� ��� �ؽ�Ʈ�� ����ϴ�.
            itemNameText.text = "";
            itemDescriptionText.text = "";
        }
    }

    private void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!IsInventoryActive)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    private void OpenInventory()
    {
        mInventoryBase.SetActive(true);
        IsInventoryActive = true;
    }

    public void CloseInventory()
    {
        mInventoryBase.SetActive(false);
        IsInventoryActive = false;
    }

    public void AcquireItem(Item item, InventorySlot targetSlot, int count = 1)
    {
        if (item.CanOverlap)
        {
            if (targetSlot.Item != null && targetSlot.Item.ItemID == item.ItemID)
            {
                targetSlot.UpdateSlotCount(count);
            }
        }
        else
        {
            targetSlot.AddItem(item, count);
        }
    }

    public void AcquireItem(Item item, int count = 1)
    {
        if (item.CanOverlap)
        {
            for (int i = 0; i < mSlots.Length; i++)
            {
                if (mSlots[i].Item != null && mSlots[i].Item.ItemID == item.ItemID)
                {
                    mSlots[i].UpdateSlotCount(count);
                    return;
                }
            }
        }
        for (int i = 0; i < mSlots.Length; i++)
        {
            if (mSlots[i].Item == null)
            {
                mSlots[i].AddItem(item, count);
                return;
            }
        }
    }

    public void ClearAllSlots()
    {
        for (int i = 0; i < mSlots.Length; i++)
        {
            mSlots[i].ClearSlot();
        }
    }

    public InventorySlot[] Slots
    {
        get { return mSlots; }
    }

    public List<InventorySlotData> GetInventoryData()
    {
        inventoryData.Clear();
        foreach (var slot in mSlots)
        {
            if (slot.Item != null)
            {
                inventoryData.Add(new InventorySlotData { itemID = slot.Item.ItemID, itemCount = slot.ItemCount });
            }
            else
            {
                inventoryData.Add(new InventorySlotData { itemID = -1, itemCount = 0 }); // �� ������ itemID�� -1�� ǥ��
            }
        }
        return inventoryData;
    }

    // �κ��丮 ������ �ҷ�����
    public void SetInventoryData(List<InventorySlotData> data)
    {
        ClearAllSlots();
        for (int i = 0; i < Mathf.Min(data.Count, mSlots.Length); i++)
        {
            if (data[i].itemID != -1)
            {
                Item item = ItemDatabase.GetItemByID(data[i].itemID);
                if (item != null)
                {
                    mSlots[i].AddItem(item, data[i].itemCount);
                }
            }
        }
    }
}