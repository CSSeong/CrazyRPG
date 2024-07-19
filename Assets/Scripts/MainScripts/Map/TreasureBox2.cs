using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox2 : Boxbase
{
    [Header("기본 보물 상자")]
    [SerializeField]
    private int coinCount;
    [SerializeField]
    private List<Item> ConsumableItem;
    [SerializeField]
    private List<Item> SkillItem;


    private PlayerData playerData;
    private InventoryMain inventoryMain;

    private void Awake()
    {
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        inventoryMain = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryMain>();
    }

    public override void UpdateCollision()
    {
        float randomValue = Random.Range(0f, 1f);

        if (playerData != null)
        {
            playerData.Coin += coinCount;
            Destroy(gameObject);

            switch(randomValue)
            {
                case float n when (n < 0.05f):
                    BlessingManager.instance.CurseSelection.gameObject.SetActive(true);
                    break;
                case float n when (n >= 0.05f && n < 0.35f):
                    BlessingManager.instance.CurseSelection.gameObject.SetActive(true);
                    BlessingManager.instance.BlessingSelection.gameObject.SetActive(true);
                    break;
                case float n when (n >= 0.35f && n < 0.75f):
                    int randomIndex01 = Random.Range(0, ConsumableItem.Count);
                    inventoryMain.AcquireItem(ConsumableItem[randomIndex01], 1);
                    Debug.Log(ConsumableItem[randomIndex01].ItemName + "획득");
                    break;
                case float n when (n >= 0.75f && n <= 1.0f):
                    int randomIndex02 = Random.Range(0, SkillItem.Count);
                    inventoryMain.AcquireItem(SkillItem[randomIndex02], 1);
                    Debug.Log(SkillItem[randomIndex02].ItemName + "획득");
                    break;
                default:
                    Debug.Log("잘못된 값");
                    break;
            }
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
