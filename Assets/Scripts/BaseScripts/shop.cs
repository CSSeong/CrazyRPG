using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();
    private PlayerData player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
    }

    public void BuyItem(Item item)
    {
        if(player.Coin >= item.itemCost)
        {
            player.Coin -= item.itemCost;
            Debug.Log("�������� �����߽��ϴ�: " + item.itemName);
            //������ �߰� �ڵ�
        }
        else
        {
            Debug.Log("��尡 �����մϴ�!");
        }
    }

    public void StealItem(Item item)
    {
        Debug.Log("�������� ���ƽ��ϴ�: " + item.itemName);
        // ������ �߰� �ڵ�
        RemoveItemFromShop(item); // �������� ������ ����
    }

    public void InitializeShop()
    {
        // ���� ������ �߰� (�������� ���� ���ӿ��� �ٸ� ������� �߰��� �� ����)
    }

    private void RemoveItemFromShop(Item item)
    {
        itemList.Remove(item);
    }
}
