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

    [Header("������ ���Կ� �ִ� UI ������Ʈ")]
    [SerializeField]
    private Image mItemImage;
    [SerializeField]
    private TextMeshProUGUI mTextCount;

    //������ �߰�
    public void AddItem(Item item, int count = 1)
    {
        mItem = item;
        mItemCount = count;
        mItemImage.sprite = mItem.Image;
        mTextCount.text = mItemCount.ToString();
    }

    //������ ���� ������Ʈ
    public void UpdateSlotCount(int count)
    {
        mItemCount += count;
        mTextCount.text = mItemCount.ToString();

        if(mItemCount <= 0)
        {
            ClearSlot();
        }
    }

    //������ ����
    public void ClearSlot()
    {
        mItem = null;
        mItemCount = 0;
        mItemImage.sprite = null;
        mTextCount.text = "";
    }

}
