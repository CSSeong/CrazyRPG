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
    [Header("고유한 아이템의 ID(중복불가)")]
    [SerializeField]
    private int mItemID;

    public int ItemID
    {
        get
        {
            return mItemID;
        }
    }

    [Header("아이템의 중첩이 가능한가?")]
    [SerializeField]
    private bool mCanOverlap;

    public bool CanOverlap
    {
        get
        {
            return mCanOverlap;
        }
    }

    [Header("사용이 가능한 아이템인가?")]
    [SerializeField]
    private bool mIsInteractivity;

    public bool IsInteractivity
    {
        get
        {
            return mIsInteractivity;
        }
    }

    [Header("아이템을 사용하면 사라지는가?")]
    [SerializeField]
    private bool mIsConsumable;

    public bool IsConsumable
    {
        get
        {
            return mIsConsumable;
        }
    }

    [Header("아이템의 타입")]
    [SerializeField]
    private ItemType mItemType;

    public ItemType Type
    {
        get
        {
            return mItemType;
        }
    }

    [Header("인벤토리에서 보여질 아이템의 이미지")]
    [SerializeField]
    private Sprite mItemImage;

    public Sprite Image
    {
        get
        {
            return mItemImage;
        }
    }
}
