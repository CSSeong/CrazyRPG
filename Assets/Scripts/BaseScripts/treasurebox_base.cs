using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class treasurebox_base : MonoBehaviour
{
    [Header("∫£¿ÃΩ∫ ∫∏π∞ ªÛ¿⁄")]
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

    public void ClickBox()
    {
        playerData.Coin += 150;
        float randomValue = Random.Range(0f, 1f);

        if (playerData != null)
        {
            switch (randomValue)
            {
                case float n when (n < 0.05f):
                    BlessingManager.instance.CurseSelection.gameObject.SetActive(true);
                    break;
                case float n when (n >= 0.05f && n < 0.1f):
                    int randomIndex01 = Random.Range(0, SkillItem.Count);
                    inventoryMain.AcquireItem(SkillItem[randomIndex01], 1);
                    Debug.Log(SkillItem[randomIndex01].ItemName + "»πµÊ");
                    break;
                case float n when (n >= 0.1f && n < 0.25f):
                    int randomIndex02 = Random.Range(0, ConsumableItem.Count);
                    inventoryMain.AcquireItem(ConsumableItem[randomIndex02], 1);
                    Debug.Log(ConsumableItem[randomIndex02].ItemName + "»πµÊ");
                    break;
                case float n when (n >= 0.25f && n < 0.55f):
                    BlessingManager.instance.CurseSelection.gameObject.SetActive(true);
                    BlessingManager.instance.BlessingSelection.gameObject.SetActive(true);
                    break;
                case float n when (n >= 0.55f && n <= 1.0f):
                    playerData.Coin += 50;
                    break;
                default:
                    Debug.Log("¿ﬂ∏¯µ» ∞™");
                    break;
            }
        }
        gameObject.SetActive(false);
    }
}
