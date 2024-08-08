using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;
    public List<AbilitySlotData> abilitySlots;
    public int selectedSlotIndex = 0; // 현재 선택된 슬롯을 추적

    [SerializeField]
    private TextMeshProUGUI[] _abilityName;
    [SerializeField]
    private TextMeshProUGUI[] _abilityDescription;
    [SerializeField]
    private TextMeshProUGUI[] _abilityLevel;
    [SerializeField]
    private TextMeshProUGUI[] _requiredSPText;
    [SerializeField]
    private Button[] _upgradeButton;
    [SerializeField]
    private Image[] _abilityIcon;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DisplayAbilities();
        InitializeButtons();
    }

    private void DisplayAbilities()
    {
        // Ensure that the index is within the range of abilitySlots
        if (selectedSlotIndex < 0 || selectedSlotIndex >= abilitySlots.Count)
        {
            Debug.LogError("Selected slot index is out of range.");
            return;
        }

        var slot = abilitySlots[selectedSlotIndex];
        int totalUIElements = Mathf.Min(_abilityName.Length, _abilityDescription.Length, _abilityLevel.Length, _requiredSPText.Length, _upgradeButton.Length, _abilityIcon.Length);
        int count = Mathf.Min(slot.abilities.Count, totalUIElements);

        for (int abilityIndex = 0; abilityIndex < count; abilityIndex++)
        {
            var ability = slot.abilities[abilityIndex];
            _abilityName[abilityIndex].text = ability.abilityName;
            _abilityDescription[abilityIndex].text = ability.description;
            _abilityLevel[abilityIndex].text = $"LV.{ability.Level}";
            _requiredSPText[abilityIndex].text = $"SP: {ability.requiredSP}";
            _abilityIcon[abilityIndex].sprite = ability.icon;
            _upgradeButton[abilityIndex].interactable = (!ability.IsMaxLevel() && SaveManager.instance.nowPlayer.SP >= ability.requiredSP);
        }

        // Hide UI elements if there are not enough abilities to display
        for (int i = count; i < totalUIElements; i++)
        {
            _abilityName[i].text = "";
            _abilityDescription[i].text = "";
            _abilityLevel[i].text = "";
            _requiredSPText[i].text = "";
            _abilityIcon[i].sprite = null;
            _upgradeButton[i].interactable = false;
        }
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < _upgradeButton.Length; i++)
        {
            int abilityIndex = i;
            _upgradeButton[i].onClick.AddListener(() => UpgradeAbility(abilityIndex));
        }
    }

    private void UpgradeAbility(int abilityIndex)
    {
        if (selectedSlotIndex < 0 || selectedSlotIndex >= abilitySlots.Count)
        {
            Debug.LogError("Selected slot index is out of range.");
            return;
        }

        var slot = abilitySlots[selectedSlotIndex];
        if (abilityIndex < slot.abilities.Count)
        {
            Ability ability = slot.abilities[abilityIndex];
            if (ability.CanUpgrade() && SaveManager.instance.nowPlayer.SP >= ability.requiredSP)
            {
                SaveManager.instance.nowPlayer.SP -= ability.requiredSP;
                ability.Upgrade();
                Debug.Log($"{ability.abilityName} 업그레이드 완료. 현재 레벨: {ability.Level}");
                DisplayAbilities(); // UI 업데이트
                SaveManager.instance.SaveData(); // 데이터를 업데이트한 후 저장
            }
            else
            {
                Debug.LogWarning("SP가 부족합니다.");
            }
        }
    }

    public void SetSelectedSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < abilitySlots.Count)
        {
            selectedSlotIndex = slotIndex;
            DisplayAbilities();
        }
        else
        {
            Debug.LogError("Invalid slot index.");
        }
    }

    public List<AbilitySlotData> GetAbilitySlotsData()
    {
        return abilitySlots;
    }

    public void SetAbilitySlotsData(List<AbilitySlotData> slotDataList)
    {
        abilitySlots = slotDataList;
        DisplayAbilities();
    }

    public void ResetAbilities()
    {
        foreach (var slot in abilitySlots)
        {
            foreach (var ability in slot.abilities)
            {
                ability.Reset();
            }
        }
        DisplayAbilities();
    }
}
