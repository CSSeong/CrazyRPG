using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI itemNameText; 
    [SerializeField] 
    private Image itemImage; 
    [SerializeField] 
    private TextMeshProUGUI priceText; 

    private Item item;
    private ShopManager shopManager;

    public void Setup(Item itemData, ShopManager manager)
    {
        this.item = itemData;
        this.shopManager = manager;

        itemNameText.text = item.ItemName;
        itemImage.sprite = item.Image;
        priceText.text = item.Price.ToString();
    }

    public void ClearSlot()
    {
        itemNameText.text = "";
        itemImage.sprite = null;
        priceText.text = "";
    }
}
