using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private PlayerHP playerHP;

    public float MaxLightGage => maxLightGage;
    public float CurrentLightGage => currentLightGage;

    private Material maskMaterial;
    
    private void Awake()
    {
        currentLightGage = maxLightGage;

        maskMaterial = maskImage.GetComponent<Image>().material;
        playerHP = GetComponentInChildren<PlayerHP>();
    }

    private void Update()
    {
        Vector3 maskPosition = mainCamera.WorldToScreenPoint(player.position) + new Vector3(0, 60, 0);
        maskImage.position = maskPosition;

        if(currentLightGage > 0)
        {
            currentLightGage -= Time.deltaTime * 1;
        }
        else if(currentLightGage < 0)
        {
            currentLightGage = 0.1f;
        }

        float normalizedLightGage = currentLightGage / maxLightGage;
        maskMaterial.SetFloat("_LightGage", normalizedLightGage);

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(RechargeLightGage());
        }
    }

    private IEnumerator RechargeLightGage()
    {
        float rechargeSpeed = 50f; 

        while (currentLightGage < maxLightGage)
        {
            currentLightGage += rechargeSpeed * Time.deltaTime;
            yield return null;
        }

        currentLightGage = maxLightGage;
    }

}
