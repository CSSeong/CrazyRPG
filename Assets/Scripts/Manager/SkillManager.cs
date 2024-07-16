using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    [Header("Ω∫≈≥ ΩΩ∑‘ UI")]
    [SerializeField]
    private SkillSlot skillSlot;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void AddSkill(Item item)
    {
        if (skillSlot.SkillItem == null)
        {
            skillSlot.AddSkill(item);
            return;
        }
        Debug.Log("Ω∫≈≥ ΩΩ∑‘¿Ã ∞°µÊ √°Ω¿¥œ¥Ÿ.");
    }

    public void RemoveSkill()
    {
        if(skillSlot.SkillItem == null)
        {
            Debug.Log("Ω∫≈≥ ΩΩ∑‘¿Ã ∫ÒæÓ¿÷Ω¿¥œ¥Ÿ.");
        }
        else
        {
            skillSlot.ClearSlot();
            return;
        }  
    }

    public void OnSkillSlotClicked()
    {
        SkillSlot clickedSlot = skillSlot;

        if (clickedSlot.SkillItem != null)
        {
            clickedSlot.UseSkill();
        }
    }

    public bool IsSkillSlotEmpty()
    {
        return skillSlot.SkillItem == null;
    }
}
