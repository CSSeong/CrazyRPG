using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    private Item mItem;
    public Item Item
    {
        get
        {
            return mItem;
        }
    }
    private int mItemCount;

    public int ItemCount
    {
        get
        {
            return mItemCount;
        }
        set
        {
            mItemCount = value;
        }
    }

    [Header("아이템 슬롯에 있는 UI 오브젝트")]
    [SerializeField]
    private Image mItemImage;
    [SerializeField]
    private TextMeshProUGUI mTextCount;

    //아이템 추가
    public void AddItem(Item item, int count = 1)
    {
        mItem = item;
        mItemCount = count;
        mItemImage.sprite = mItem.Image;
        UpdateItemCountText();
    }

    //아이템 개수 업데이트
    public void UpdateSlotCount(int count)
    {
        mItemCount += count;
        UpdateItemCountText();

        if (mItemCount <= 0)
        {
            ClearSlot();
        }
    }

    //아이템 삭제
    public void ClearSlot()
    {
        mItem = null;
        mItemCount = 0;
        mItemImage.sprite = null;
        mTextCount.text = "";
    }

    // UI 업데이트를 위한 LateUpdate 메서드 추가
    private void LateUpdate()
    {
        UpdateItemCountText();
    }

    // 아이템 개수 텍스트 업데이트 메서드
    private void UpdateItemCountText()
    {
        if (mItem != null)
        {
            mTextCount.text = mItemCount.ToString();
        }
        else
        {
            mTextCount.text = "";
        }
    }
}