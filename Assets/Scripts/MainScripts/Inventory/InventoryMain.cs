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

    [Header("아이템 정보 표시 UI")]
    [SerializeField]
    private TextMeshProUGUI itemNameText;
    [SerializeField]
    private TextMeshProUGUI itemDescriptionText;

    [Header("아이템 버튼 UI")]
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
            DontDestroyOnLoad(gameObject); // 이 라인이 인벤토리 오브젝트를 씬 전환 시 삭제되지 않도록 합니다.
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
        // 각 슬롯에 클릭 이벤트를 등록합니다.
        for (int i = 0; i < mSlots.Length; i++)
        {
            int slotIndex = i; // 슬롯 인덱스를 클로저로 가져오기 위해 사용합니다.
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

            // 등록하기 버튼은 아이템이 상호작용 가능하고 스킬 슬롯이 비어 있을 때만 활성화
            registButton.gameObject.SetActive(true);
            registButton.interactable = clickedSlot.Item.IsInteractivity && SkillManager.Instance.IsSkillSlotEmpty();

            // 해제하기 버튼은 스킬 슬롯이 비어 있지 않을 때만 활성화
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

            // 소모성 아이템인 경우 인벤토리에서 제거합니다.
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
            // 버튼의 활성화 여부에 따라 활성화/비활성화를 처리
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
                inventoryData.Add(new InventorySlotData { itemID = -1, itemCount = 0 }); // 빈 슬롯은 itemID를 -1로 표시
            }
        }
        return inventoryData;
    }

    // 인벤토리 데이터 불러오기
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
