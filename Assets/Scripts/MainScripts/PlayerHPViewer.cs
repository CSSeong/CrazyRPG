using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHPViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerHP playerHP;
    private Slider sliderHP;

    private TextMeshProUGUI HpText;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
        HpText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        sliderHP.value = playerHP.CurrentHP / playerHP.MaxHP;
        HpText.text = $"{Mathf.RoundToInt(playerHP.CurrentHP)} / {Mathf.RoundToInt(playerHP.MaxHP)}";
    }
}
