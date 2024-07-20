using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable = 64,
    Skill = 128,
    Story = 256
}

[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Item")]
public class Item : ScriptableObject
{
    [Header("������ �������� ID(�ߺ��Ұ�)")]
    [SerializeField]
    private int mItemID;

    public int ItemID
    {
        get
        {
            return mItemID;
        }
    }
    [Header("������ ����")]
    [SerializeField]
    private int mPrice;

    public int Price
    {
        get { return mPrice; }
    }

    [Header("�������� �̸��� ����")]
    [SerializeField]
    private string mItemName;
    public string ItemName
    {
        get
        {
            return mItemName;
        }
    }
    [SerializeField]
    private string mItemDescription;
    public string ItemDescription
    {
        get
        {
            return mItemDescription;
        }
    }
    [Header("�������� ��ø�� �����Ѱ�?")]
    [SerializeField]
    private bool mCanOverlap;

    public bool CanOverlap
    {
        get
        {
            return mCanOverlap;
        }
    }

    [Header("����� ������ �������ΰ�?")]
    [SerializeField]
    private bool mIsInteractivity;

    public bool IsInteractivity
    {
        get
        {
            return mIsInteractivity;
        }
    }

    [Header("�������� ����ϸ� ������°�?")]
    [SerializeField]
    private bool mIsConsumable;

    public bool IsConsumable
    {
        get
        {
            return mIsConsumable;
        }
    }

    [Header("�������� Ÿ��")]
    [SerializeField]
    private ItemType mItemType;

    public ItemType Type
    {
        get
        {
            return mItemType;
        }
    }

    [Header("�κ��丮���� ������ �������� �̹���")]
    [SerializeField]
    private Sprite mItemImage;

    public Sprite Image
    {
        get
        {
            return mItemImage;
        }
    }

    public void Use()
    {
        switch (mItemID)
        {
            case 1:
                Debug.Log("���� ������ ���");
                SkillManager.Instance._playerLight.Recharge();
                break;
            case 2:
                Debug.Log("���͸� ������ ���");
                break;
            case 3:
                Debug.Log("ȸ���� ������ ���");
                break;
            case 4:
                Debug.Log("���� ������ ���");
                break;
        }
    }
}