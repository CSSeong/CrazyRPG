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
    public float CurrentLightGage
    {
        get { return currentLightGage; }
        set { currentLightGage = value; }
    }

    private Material maskMaterial;
    
    private void Awake()
    {
        currentLightGage = maxLightGage;

        maskMaterial = maskImage.GetComponent<Image>().material;
        playerHP = GetComponentInChildren<PlayerHP>();
        if (playerHP == null)
        {
            Debug.LogError("PlayerHP 컴포넌트를 찾을 수 없습니다.");
        }
    }

    private void Update()
    {
        if (playerHP == null)
        {
            return;
        }

        Vector3 maskPosition = mainCamera.WorldToScreenPoint(player.position) + new Vector3(0, 60, 0);
        maskImage.position = maskPosition;

        if(currentLightGage > 0)
        {
            currentLightGage -= Time.deltaTime * 10;
        }
        else if(currentLightGage <= 0)
        {
            playerHP.TakeDamage(Time.deltaTime * 10);
        }

        currentLightGage = Mathf.Max(currentLightGage, 0);

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
