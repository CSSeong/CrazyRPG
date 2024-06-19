using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLightViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerLight playerLight;
    private Slider sliderLight;

    private void Awake()
    {
        sliderLight = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderLight.value = playerLight.CurrentLightGage / playerLight.MaxLightGage;
    }
}
