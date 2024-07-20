using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : Boxbase
{
    [Header("초반 상자")]
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
                inventoryMain.AcquireItem(testItem, 3); // 아이템을 3개 추가
            }

            BlessingManager.instance.BlessingSelection.gameObject.SetActive(true);
            BlessingManager.instance.CurseSelection.gameObject.SetActive(true);
            Debug.Log("장작2개, 축복1개 획득");

            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("PlayerData를 찾을 수 없습니다.");
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
