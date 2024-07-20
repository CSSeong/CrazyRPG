using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSlot : MonoBehaviour
{
    private Item skillItem;
    private int skillItemCount;
    public Item SkillItem
    {
        get
        {
            return skillItem;
        }
    }

    [Header("��ų ���Կ� �ִ� UI ������Ʈ")]
    [SerializeField]
    private Image skillImage;
    [SerializeField]
    private TextMeshProUGUI itemCountText;

    // ��ų �߰�
    public void AddSkill(Item item, int count)
    {
        skillItem = item;
        skillItemCount = count;
        skillImage.sprite = skillItem.Image;
        UpdateItemCountText();
    }

    // ��ų ����
    public void ClearSlot()
    {
        skillItem = null;
        skillItemCount = 0;
        skillImage.sprite = null;
        UpdateItemCountText();
    }

    // ��ų ���
    public void UseSkill()
    {
        if (skillItem != null)
        {
            skillItem.Use();
            InventoryMain.Instance.ReduceItemCount(skillItem);
            //���� ������Ʈ
            skillItemCount = InventoryMain.Instance.GetItemCount(skillItem);
            UpdateItemCountText();

            // ������ ������ 0�̸� ���� ����
            if (skillItemCount <= 0)
            {
                ClearSlot();
            }
        }
    }

    private void UpdateItemCountText()
    {
        if (skillItem != null && skillItemCount > 1)
        {
            itemCountText.text = skillItemCount.ToString();
        }
        else
        {
            itemCountText.text = "";
        }
    }
}