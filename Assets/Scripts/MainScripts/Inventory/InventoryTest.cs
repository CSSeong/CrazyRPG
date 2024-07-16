using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public InventoryMain inventoryMain; // �κ��丮 ���� ��ũ��Ʈ ����
    public Item testItem; // �׽�Ʈ�� ������
    public Item testItem2;

    void Start()
    {
        // �κ��丮�� ������ �߰�
        AddTestItemToInventory();
    }

    void AddTestItemToInventory()
    {
        if (inventoryMain != null && testItem != null)
        {
            inventoryMain.AcquireItem(testItem, 12); // �������� 12�� �߰�
        }
        if (inventoryMain != null && testItem2 != null)
        {
            inventoryMain.AcquireItem(testItem2, 10); // �������� 12�� �߰�
        }
    }
}
