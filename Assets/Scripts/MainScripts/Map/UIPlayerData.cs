using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerData : MonoBehaviour
{

    [Header("Gold")]
    [SerializeField]
    private TextMeshProUGUI textGold;

    public void SetGold(int Gold)
    {
        textGold.text = $"{Gold}";
    }

}
