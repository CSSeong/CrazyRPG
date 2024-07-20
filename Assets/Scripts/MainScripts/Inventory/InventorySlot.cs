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
        UpdateItemCountText();
    }

    //������ ���� ������Ʈ
    public void UpdateSlotCount(int count)
    {
        mItemCount += count;
        UpdateItemCountText();

        if (mItemCount <= 0)
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

    // UI ������Ʈ�� ���� LateUpdate �޼��� �߰�
    private void LateUpdate()
    {
        UpdateItemCountText();
    }

    // ������ ���� �ؽ�Ʈ ������Ʈ �޼���
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