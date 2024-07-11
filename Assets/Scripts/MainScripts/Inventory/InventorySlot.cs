using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
        mTextCount.text = mItemCount.ToString();
    }

    //아이템 개수 업데이트
    public void UpdateSlotCount(int count)
    {
        mItemCount += count;
        mTextCount.text = mItemCount.ToString();

        if(mItemCount <= 0)
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

}
