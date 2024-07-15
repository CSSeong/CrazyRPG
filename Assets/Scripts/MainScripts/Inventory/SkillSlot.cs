using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSlot : MonoBehaviour
{
    private Item skillItem;
    public Item SkillItem
    {
        get
        {
            return skillItem;
        }
    }
    private InventorySlot skillCount;
    public InventorySlot SkillCount
    {
        get
        {
            return skillCount;
        }
    }

    [Header("스킬 슬롯에 있는 UI 오브젝트")]
    [SerializeField]
    private Image skillImage;

    // 스킬 추가
    public void AddSkill(Item item)
    {
        skillItem = item;
        skillImage.sprite = skillItem.Image;
    }

    // 스킬 삭제
    public void ClearSlot()
    {
        skillItem = null;
        skillImage.sprite = null;
    }

    // 스킬 사용
    public void UseSkill()
    {
        if (skillItem != null)
        {
            skillItem.Use();
        }
    }
}
