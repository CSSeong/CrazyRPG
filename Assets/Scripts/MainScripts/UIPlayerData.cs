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

    public void SetWood(int WoodCount)
    {
        textFireWood.text = $"{WoodCount}";
    }
}
