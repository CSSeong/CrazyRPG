using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_base : MonoBehaviour
{
    [SerializeField]
    private Shop shop;

    private void Awake()
    {
        SaveManager.instance.LoadData();
    }

    public void ResetShop()
    {
        shop.itemList.Clear();
        shop.InitializeShop();
    }
}
