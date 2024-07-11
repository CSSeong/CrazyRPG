using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox3 : Boxbase
{
    [Header("TreasureBox")]
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
            coinCount = Random.Range(80, 131);
            playerData.Coin += coinCount;
            Debug.Log($"{gameObject.name} 보물상자 충돌: 코인 {coinCount}개 추가됨.");
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
