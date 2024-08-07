using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class InventoryBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject mInventoryBase;
    [SerializeField]
    protected GameObject mInventorySlotsParent;
    [SerializeField]
    protected InventorySlot[] mSlots;

    protected void Awake()
    {
        if(mInventoryBase.activeSelf)
        {
            mInventoryBase.SetActive(false);
        }

        mSlots = mInventorySlotsParent.GetComponentsInChildren<InventorySlot>();
    }
}
