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

    [Header("스킬 슬롯에 있는 UI 오브젝트")]
    [SerializeField]
    private Image skillImage;
    [SerializeField]
    private TextMeshProUGUI itemCountText;

    // 스킬 추가
    public void AddSkill(Item item, int count)
    {
        skillItem = item;
        skillItemCount = count;
        skillImage.sprite = skillItem.Image;
        UpdateItemCountText();
    }

    // 스킬 삭제
    public void ClearSlot()
    {
        skillItem = null;
        skillItemCount = 0;
        skillImage.sprite = null;
        UpdateItemCountText();
    }

    // 스킬 사용
    public void UseSkill()
    {
        if (skillItem != null)
        {
            skillItem.Use();
            InventoryMain.Instance.ReduceItemCount(skillItem);
            //슬롯 업데이트
            skillItemCount = InventoryMain.Instance.GetItemCount(skillItem);
            UpdateItemCountText();

            // 아이템 개수가 0이면 슬롯 비우기
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