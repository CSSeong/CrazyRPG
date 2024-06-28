using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLightViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerLight playerLight;
    private Slider sliderLight;
    private TextMeshProUGUI lightText;

    private void Awake()
    {
        sliderLight = GetComponent<Slider>();
        lightText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        sliderLight.value = playerLight.CurrentLightGage / playerLight.MaxLightGage;
        lightText.text = $"{Mathf.RoundToInt(playerLight.CurrentLightGage)} / {Mathf.RoundToInt(playerLight.MaxLightGage)}";
    }
}
