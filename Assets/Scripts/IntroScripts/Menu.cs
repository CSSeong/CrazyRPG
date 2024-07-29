using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Toggle[] toggles;
    [SerializeField]
    private GameObject[] panels;

    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(delegate { OnToggleChanged(toggles[0]); });
        toggles[1].onValueChanged.AddListener(delegate { OnToggleChanged(toggles[1]); });
        UpdatePanels();
    }

    private void OnToggleChanged(Toggle changedToggle)
    {
        UpdatePanels();
    }

    private void UpdatePanels()
    {
        panels[0].SetActive(toggles[0].isOn);
        panels[1].SetActive(toggles[1].isOn);
    }
}
