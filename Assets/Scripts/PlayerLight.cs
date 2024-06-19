using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [SerializeField]
    private RectTransform maskImage;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Camera mainCamera;

    private float maxLightGage = 100;
    private float currentLightGage;

    public float MaxLightGage => maxLightGage;
    public float CurrentLightGage => currentLightGage;

    private void Awake()
    {
        currentLightGage = maxLightGage;
    }

    private void Update()
    {
        Vector3 maskPosition = mainCamera.WorldToScreenPoint(player.position);
        maskImage.position = maskPosition;

        currentLightGage -= Time.deltaTime * 1;

        if (Input.GetKeyDown(KeyCode.X))
        {
            UseItem();
        }
    }

    private void UseItem()
    {
        currentLightGage = maxLightGage;
    }
}
