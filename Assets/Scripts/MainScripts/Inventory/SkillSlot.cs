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

    [Header("��ų ���Կ� �ִ� UI ������Ʈ")]
    [SerializeField]
    private Image skillImage;

    // ��ų �߰�
    public void AddSkill(Item item)
    {
        skillItem = item;
        skillImage.sprite = skillItem.Image;
    }

    // ��ų ����
    public void ClearSlot()
    {
        skillItem = null;
        skillImage.sprite = null;
    }

    // ��ų ���
    public void UseSkill()
    {
        if (skillItem != null)
        {
            skillItem.Use();
        }
    }
}
