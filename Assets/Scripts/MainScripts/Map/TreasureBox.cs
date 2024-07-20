using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : Boxbase
{
    [Header("�ʹ� ����")]
    [SerializeField]
    private Item testItem;

    private InventoryMain inventoryMain;

    private PlayerData playerData;

    private void Awake()
    {
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        inventoryMain = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryMain>();
    }

    public override void UpdateCollision()
    {
        if (playerData != null)
        {
            if (inventoryMain != null && testItem != null)
            {
                inventoryMain.AcquireItem(testItem, 3); // �������� 3�� �߰�
            }

            BlessingManager.instance.BlessingSelection.gameObject.SetActive(true);
            BlessingManager.instance.CurseSelection.gameObject.SetActive(true);
            Debug.Log("����2��, �ູ1�� ȹ��");

            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("PlayerData�� ã�� �� �����ϴ�.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateCollision();
        }
    }

}
