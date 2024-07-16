using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public InventoryMain inventoryMain; // 인벤토리 메인 스크립트 참조
    public Item testItem; // 테스트용 아이템
    public Item testItem2;

    void Start()
    {
        // 인벤토리에 아이템 추가
        AddTestItemToInventory();
    }

    void AddTestItemToInventory()
    {
        if (inventoryMain != null && testItem != null)
        {
            inventoryMain.AcquireItem(testItem, 12); // 아이템을 12개 추가
        }
        if (inventoryMain != null && testItem2 != null)
        {
            inventoryMain.AcquireItem(testItem2, 10); // 아이템을 12개 추가
        }
    }
}
