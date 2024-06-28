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
        set => coin = Mathf.Clamp(value, 0, 9999);
    }

    private int fireWood = 0;
    public int FireWood
    {
        get => fireWood;
        set
        {
            fireWood = value;
            uiplayerData.SetWood(fireWood);
        }
    }
}
