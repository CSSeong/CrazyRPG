using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerData : MonoBehaviour
{
    [Header("FireWood")]
    [SerializeField]
    private TextMeshProUGUI textFireWood;

    [Header("Gold")]
    [SerializeField]
    private TextMeshProUGUI textGold;

    public void SetWood(int WoodCount)
    {
        textFireWood.text = $"{WoodCount}";
    }

    public void SetGold(int GoldCount)
    {
        textGold.text = $"{GoldCount}";
    }
}
