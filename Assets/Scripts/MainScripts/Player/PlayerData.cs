using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private UIPlayerData uiplayerData;

    private int coin = 0;
    public int Coin
    {
        get => coin;
        set
        {
            coin = Mathf.Clamp(value, 0, 9999);
            uiplayerData.SetGold(coin);
            SaveManager.instance.nowPlayer.coin = coin; // SaveManager�� coin ���� �Ҵ�
        }
    }

    private void Awake()
    {
        // ���� ���� �� SaveManager���� ������ �ҷ�����
        LoadDataFromSaveManager();
    }

    private void LoadDataFromSaveManager()
    {
        coin = SaveManager.instance.nowPlayer.coin;
        uiplayerData.SetGold(coin);
    }
}
