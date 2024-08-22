using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;
    public List<AbilitySlotData> abilitySlots;
    public int selectedSlotIndex = 0;

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
        InitializeButtons();
    }

    private void Update()
    {
        DisplayAbilities();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Shift 키 입력 감지됨.");
            ActivateAbility(5);
        }
    }

    private void DisplayAbilities()
    {
        // UI 요소가 null이거나 비활성화된 경우
        /*if (_abilityName == null || _abilityName.Length == 0 || _abilityName[0] == null || !_abilityName[0].gameObject.activeInHierarchy ||
            _abilityDescription == null || _abilityDescription.Length == 0 || _abilityDescription[0] == null || !_abilityDescription[0].gameObject.activeInHierarchy ||
            _abilityLevel == null || _abilityLevel.Length == 0 || _abilityLevel[0] == null || !_abilityLevel[0].gameObject.activeInHierarchy ||
            _requiredSPText == null || _requiredSPText.Length == 0 || _requiredSPText[0] == null || !_requiredSPText[0].gameObject.activeInHierarchy ||
            _upgradeButton == null || _upgradeButton.Length == 0 || _upgradeButton[0] == null || !_upgradeButton[0].gameObject.activeInHierarchy ||
            _abilityIcon == null || _abilityIcon.Length == 0 || _abilityIcon[0] == null || !_abilityIcon[0].gameObject.activeInHierarchy)
        {
            Debug.LogWarning("UI 요소가 비활성화되었거나 null입니다. UI 업데이트를 건너뜁니다.");
            return;
        }*/

        // 선택된 슬롯 인덱스가 유효한지 확인
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
            if (_abilityName[abilityIndex] != null) _abilityName[abilityIndex].text = ability.abilityName;
            if (_abilityDescription[abilityIndex] != null) _abilityDescription[abilityIndex].text = ability.description;
            if (_abilityLevel[abilityIndex] != null) _abilityLevel[abilityIndex].text = $"LV.{ability.Level}";
            if (_requiredSPText[abilityIndex] != null) _requiredSPText[abilityIndex].text = $"SP: {ability.requiredSP}";
            if (_abilityIcon[abilityIndex] != null) _abilityIcon[abilityIndex].sprite = ability.icon;
            if (_upgradeButton[abilityIndex] != null) _upgradeButton[abilityIndex].interactable = (!ability.IsMaxLevel() && SaveManager.instance.nowPlayer.SP >= ability.requiredSP);
        }

        // 남은 UI 요소를 비워 놓기
        for (int i = count; i < totalUIElements; i++)
        {
            if (_abilityName[i] != null) _abilityName[i].text = "";
            if (_abilityDescription[i] != null) _abilityDescription[i].text = "";
            if (_abilityLevel[i] != null) _abilityLevel[i].text = "";
            if (_requiredSPText[i] != null) _requiredSPText[i].text = "";
            if (_abilityIcon[i] != null) _abilityIcon[i].sprite = null;
            if (_upgradeButton[i] != null) _upgradeButton[i].interactable = false;
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

    private void ActivateAbility(int abilityNumber)
    {

        Debug.Log($"ActivateAbility 호출됨. 능력 번호: {abilityNumber}");

        var slot = abilitySlots[selectedSlotIndex];
        Ability ability = slot.abilities.Find(a => a.abilityNumber == abilityNumber);

        if (ability == null)
        {
            Debug.LogWarning($"능력 번호 {abilityNumber}에 해당하는 능력을 찾을 수 없음.");
            return;
        }

        Debug.Log($"능력 찾음: {ability.abilityName}, 레벨: {ability.Level}, 해방 여부: {ability.isUnlocked}, 쿨타임 여부: {ability.isOnCooldown}");

        if (ability.isUnlocked)
        {
            if (!ability.isOnCooldown)
            {
                StartCoroutine(ActivateAbilityCoroutine(ability));
            }
            else
            {
                Debug.LogWarning("스킬이 아직 쿨타임 중입니다.");
            }
        }
        else
        {
            Debug.LogWarning("해당 능력이 해방되지 않았습니다.");
        }
    }

    public IEnumerator ActivateAbilityCoroutine(Ability ability)
    {
        Debug.Log($"ActivateAbilityCoroutine 시작됨. 능력 번호: {ability.abilityNumber}, 쿨타임 여부: {ability.isOnCooldown}, 해방 여부: {ability.isUnlocked}");

        if (ability.isOnCooldown || !ability.isUnlocked || ability.abilityNumber != 5)
        {
            Debug.Log("사용 불가");
            Debug.Log($"isOnCooldown: {ability.isOnCooldown}, isAvailable: {ability.isUnlocked}, abilityNumber: {ability.abilityNumber}");
            yield break;
        }

        Debug.Log("5번 스킬 사용");
        ability.isOnCooldown = true;
        SaveManager.instance.UpdateAbilityCooldown(ability.abilityNumber, true);

        float originalSpeed = SaveManager.instance.nowPlayer.moveSpeed;
        Debug.Log($"원래 이동 속도: {originalSpeed}, 증가된 이동 속도: {originalSpeed + 3f}");

        SaveManager.instance.UpdateMoveSpeed(originalSpeed + 3f);

        yield return new WaitForSeconds(5f);

        SaveManager.instance.UpdateMoveSpeed(originalSpeed);
        Debug.Log("이동 속도 원래대로 복구됨.");

        yield return new WaitForSeconds(ability.Cooldown);

        ability.isOnCooldown = false;
        SaveManager.instance.UpdateAbilityCooldown(ability.abilityNumber, false);

        Debug.Log("5번 스킬 쿨다운 완료");
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
                DisplayAbilities();
            }
            else
            {
                Debug.LogWarning("업그레이드 조건을 만족하지 않습니다.");
            }
        }
        else
        {
            Debug.LogError("Ability index out of range.");
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

    public List<AbilitySlotData> GetAbilitySlotsData() => abilitySlots;

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