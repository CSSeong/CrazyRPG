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
            uiplayerData.SetGold(fireWood);
            SaveManager.instance.nowPlayer.coin = coin; // SaveManager�� coin ���� �Ҵ�
        }
    }

    private int fireWood = 0;
    public int FireWood
    {
        get => fireWood;
        set
        {
            fireWood = value;
            uiplayerData.SetWood(fireWood);
            SaveManager.instance.nowPlayer.firewood = fireWood; // SaveManager�� fireWood ���� �Ҵ�
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
        fireWood = SaveManager.instance.nowPlayer.firewood;
        uiplayerData.SetWood(fireWood); // UI ����
        uiplayerData.SetGold(fireWood);
    }
}
