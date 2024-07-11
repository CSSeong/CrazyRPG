using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMain : InventoryBase
{
    public static bool IsInventoryActive = false;

    new void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {

        if(Input.GetKeyDown(KeyCode.I))
        {
            if(!IsInventoryActive)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    private void OpenInventory()
    {
        mInventoryBase.SetActive(true);
        IsInventoryActive = true;

    }

    public void CloseInventory()
    {
        mInventoryBase.SetActive(false);
        IsInventoryActive = false;

    }

    public void AcquireItem(Item item, InventorySlot targetSlot, int count = 1)
    {
        if(item.CanOverlap)
        {
            if(targetSlot.Item != null && targetSlot.Item.ItemID == item.ItemID)
            {
                targetSlot.UpdateSlotCount(count);
            }
        }
        else
        {
            targetSlot.AddItem(item, count);
        }
    }

    public void AcquireItem(Item item, int count = 1)
    {
        if (item.CanOverlap)
        {
            for (int i = 0; i < mSlots.Length; i++)
            {
                if (mSlots[i].Item != null && mSlots[i].Item.ItemID == item.ItemID)
                {
                    mSlots[i].UpdateSlotCount(count);
                    return;
                }
            }
        }
        for (int i = 0; i < mSlots.Length; i++)
        {
            if (mSlots[i].Item == null)
            {
                mSlots[i].AddItem(item, count);
                return;
            }
        }
    }
}
