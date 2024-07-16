using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("������ ��ư UI")]
    [SerializeField]
    private Button useButton;
    [SerializeField]
    private Button registButton;
    [SerializeField]
    private Button removeButton;

    private List<InventorySlotData> inventoryData = new List<InventorySlotData>();
    private string disabledScene = "IntroScene";
    private InventorySlot selectedSlot;

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
            return;
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

        useButton.onClick.AddListener(OnUseButtonClicked);
        registButton.onClick.AddListener(OnRegistButtonClicked);
        removeButton.onClick.AddListener(OnRemoveButtonClicked);
        useButton.interactable = false;
        registButton.interactable = false;
        removeButton.interactable = false;

        if (InventoryMain.IsInventoryActive)
        {
            UpdateButtonState();
        }
    }

    private void OnSlotClicked(int slotIndex)
    {
        InventorySlot clickedSlot = mSlots[slotIndex];
        selectedSlot = clickedSlot;

        if (clickedSlot.Item != null)
        {
            itemNameText.text = clickedSlot.Item.ItemName;
            itemDescriptionText.text = clickedSlot.Item.ItemDescription;

            useButton.gameObject.SetActive(true);
            useButton.interactable = clickedSlot.Item.IsInteractivity;

            // ����ϱ� ��ư�� �������� ��ȣ�ۿ� �����ϰ� ��ų ������ ��� ���� ���� Ȱ��ȭ
            registButton.gameObject.SetActive(true);
            registButton.interactable = clickedSlot.Item.IsInteractivity && SkillManager.Instance.IsSkillSlotEmpty();

            // �����ϱ� ��ư�� ��ų ������ ��� ���� ���� ���� Ȱ��ȭ
            removeButton.gameObject.SetActive(true);
            removeButton.interactable = !SkillManager.Instance.IsSkillSlotEmpty();
        }
        else
        {
            ClearSelectedItemInfo();
        }

        if (InventoryMain.IsInventoryActive)
        {
            UpdateButtonState();
        }
    }


    private void OnUseButtonClicked()
    {
        if (selectedSlot != null && selectedSlot.Item != null && selectedSlot.Item.IsInteractivity)
        {
            selectedSlot.Item.Use();

            // �Ҹ� �������� ��� �κ��丮���� �����մϴ�.
            if (selectedSlot.Item.IsConsumable)
            {
                selectedSlot.ItemCount--;
                if (selectedSlot.ItemCount <= 0)
                {
                    selectedSlot.UpdateSlotCount(-1);
                    itemNameText.text = "";
                    itemDescriptionText.text = "";
                    useButton.interactable = false;
                }
            }
        }
    }

    private void OnRegistButtonClicked()
    {
        if (selectedSlot != null && selectedSlot.Item != null && selectedSlot.Item.IsInteractivity)
        {
            SkillManager.Instance.AddSkill(selectedSlot.Item);
            if (InventoryMain.IsInventoryActive)
            {
                UpdateButtonState();
            }
        }
    }

    private void OnRemoveButtonClicked()
    {
        SkillManager.Instance.RemoveSkill();
        if (InventoryMain.IsInventoryActive)
        {
            UpdateButtonState();
        }
    }

    private void UpdateButtonState()
    {
        bool isSkillSlotEmpty = SkillManager.Instance.IsSkillSlotEmpty();

        if (selectedSlot != null && selectedSlot.Item == null)
        {
            ClearSelectedItemInfo();
        }
        else
        {
            registButton.interactable = selectedSlot != null && selectedSlot.Item != null && selectedSlot.Item.IsInteractivity && isSkillSlotEmpty;
            removeButton.interactable = selectedSlot != null && !isSkillSlotEmpty;
            // ��ư�� Ȱ��ȭ ���ο� ���� Ȱ��ȭ/��Ȱ��ȭ�� ó��
            removeButton.gameObject.SetActive(!isSkillSlotEmpty);
            registButton.gameObject.SetActive(isSkillSlotEmpty);
        }

    }

    private void ClearSelectedItemInfo()
    {
        itemNameText.text = "";
        itemDescriptionText.text = "";

        useButton.gameObject.SetActive(false);
        registButton.gameObject.SetActive(false);
        removeButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!IsInventoryActive && !IsSceneDisabled())
            {
                OpenInventory();
            }
            else if (IsInventoryActive)
            {
                CloseInventory();
            }
        }
    }

    private bool IsSceneDisabled()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        return disabledScene.Contains(currentSceneName);
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
