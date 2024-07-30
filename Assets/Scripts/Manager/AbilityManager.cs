using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> abilities;

    [SerializeField]
    private TextMeshProUGUI[] _abilityName;
    [SerializeField]
    private TextMeshProUGUI[] _abilityDescription;
    [SerializeField]
    private TextMeshProUGUI[] _abilitylevel;
    [SerializeField]
    private TextMeshProUGUI[] _requiredSPText;
    [SerializeField]
    private Button[] _upgradebutton;
    [SerializeField]
    private Image[] _abilityIcon;

    private void Start()
    {
        DisplayAbilities();
        InitializeButtons();
    }

    private void DisplayAbilities()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            _abilityName[i].text = abilities[i].abilityName;
            _abilityDescription[i].text = abilities[i].description;
            _abilitylevel[i].text = $"LV.{abilities[i].Level}";
            _requiredSPText[i].text = $"SP: {abilities[i].requiredSP}";
            _abilityIcon[i].sprite = abilities[i].icon;
            _upgradebutton[i].interactable = (!abilities[i].IsMaxLevel() && SaveManager.instance.nowPlayer.SP >= abilities[i].requiredSP);
        }
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < _upgradebutton.Length; i++)
        {
            int index = i;
            _upgradebutton[i].onClick.AddListener(() => UpgradeAbility(index));
        }
    }

    private void UpgradeAbility(int index)
    {
        Ability ability = abilities[index];
        if (ability.CanUpgrade() && SaveManager.instance.nowPlayer.SP >= ability.requiredSP)
        {
            SaveManager.instance.nowPlayer.SP -= ability.requiredSP;
            ability.Upgrade();
            Debug.Log($"{ability.abilityName} 업그레이드 완료. 현재 레벨: {ability.Level}");
            DisplayAbilities(); // UI 업데이트
        }
        else
        {
            Debug.LogWarning("SP가 부족합니다.");
        }
    }

    public List<AbilityData> GetAbilitiesData()
    {
        List<AbilityData> abilityDataList = new List<AbilityData>();
        foreach (var ability in abilities)
        {
            abilityDataList.Add(new AbilityData
            {
                abilityName = ability.abilityName,
                level = ability.Level,
                requiredSP = ability.requiredSP
            });
        }
        return abilityDataList;
    }

    public void SetAbilitiesData(List<AbilityData> abilityDataList)
    {
        foreach (var abilityData in abilityDataList)
        {
            Ability ability = abilities.Find(a => a.abilityName == abilityData.abilityName);
            if (ability != null)
            {
                ability.Level = abilityData.level;
                ability.requiredSP = abilityData.requiredSP;
            }
        }
        DisplayAbilities();
    }

    public void ResetAbilities()
    {
        foreach (var ability in abilities)
        {
            ability.Reset();
        }
        DisplayAbilities();
    }
}
