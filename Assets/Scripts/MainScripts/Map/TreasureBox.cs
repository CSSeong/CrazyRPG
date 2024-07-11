using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : Boxbase
{
    [Header("TreasureBox")]
    [SerializeField]
    private int coinCount;

    private PlayerData playerData;

    private void Awake()
    {
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
    }

    public override void UpdateCollision()
    {
        if (playerData != null)
        {
            playerData.Coin += coinCount;
            Debug.Log($"{gameObject.name} �������� �浹: ���� {coinCount}�� �߰���.");
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
