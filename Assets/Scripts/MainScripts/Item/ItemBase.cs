using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Firewood = 0,
    Battery,
    HealingPotion,
    Key,
    LightKit,
    SpringShoes,
    TeleportScroll,
    AncientExplosiveDevice,
    MagitechTerminal
}

public class ItemBase : MonoBehaviour
{
    public ItemType itemType;
    public string itemName;
    public string description;
    public Sprite icon;

    public int amount;             // 공통 속성: 수량
    public bool isUsed;            // 공통 속성: 사용 여부

    public virtual void InitializeItem()
    {

    }

    public virtual void UseItem()
    {

    }

    void Start()
    {
        InitializeItem();
    }
}
