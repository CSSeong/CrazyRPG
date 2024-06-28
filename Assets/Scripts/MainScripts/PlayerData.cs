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
            SaveManager.instance.nowPlayer.coin = coin; // SaveManager에 coin 값을 할당
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
            SaveManager.instance.nowPlayer.firewood = fireWood; // SaveManager에 fireWood 값을 할당
        }
    }

    private void Awake()
    {
        // 게임 시작 시 SaveManager에서 데이터 불러오기
        LoadDataFromSaveManager();
    }

    private void LoadDataFromSaveManager()
    {
        coin = SaveManager.instance.nowPlayer.coin;
        fireWood = SaveManager.instance.nowPlayer.firewood;
        uiplayerData.SetWood(fireWood); // UI 갱신
        uiplayerData.SetGold(fireWood);
    }
}
