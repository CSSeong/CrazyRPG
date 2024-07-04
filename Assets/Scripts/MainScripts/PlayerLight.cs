using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLight : MonoBehaviour
{
    [SerializeField]
    private RectTransform maskImage;
    public RectTransform MaskImage
    {
        get { return maskImage; }
        set { maskImage = value; }
    }
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Camera mainCamera;

    private float maxLightGage = 100;
    private float currentLightGage;
    private PlayerHP playerHP;
    private PlayerData playerData;

    private float radiusX;
    public float RadiusX
    {
        get { return radiusX; }
        set { radiusX = value; }
    }
    private float radiusY;
    public float RadiusY
    {
        get { return radiusY; }
        set { radiusY = value; }
    }

    public float MaxLightGage
    {
        get { return maxLightGage; }
        set
        {
            maxLightGage = value;
            SaveManager.instance.nowPlayer.playerlightgage_max = maxLightGage;
        }
    }
    public float CurrentLightGage
    {
        get { return currentLightGage; }
        set
        { 
            currentLightGage = value;
            SaveManager.instance.nowPlayer.playerlightgage = currentLightGage;
        }
    }

    private float lightreduction = 5;
    public float Lightreduction
    {
        get { return lightreduction; }
        set { lightreduction = value; }
    }
    private Material maskMaterial;

    private bool availability = true;
    public bool Availability
    {
        get { return availability; }
        set { availability = value; }
    }

    private void Awake()
    {
        currentLightGage = maxLightGage;

        maskMaterial = maskImage.GetComponent<Image>().material;
        playerHP = GetComponentInChildren<PlayerHP>();
        if (playerHP == null)
        {
            Debug.LogError("PlayerHP 컴포넌트를 찾을 수 없습니다.");
        }
        playerData = GetComponent<PlayerData>();

        if (SaveManager.instance != null)
        {
            currentLightGage = SaveManager.instance.nowPlayer.playerlightgage;
            maxLightGage = SaveManager.instance.nowPlayer.playerlightgage_max;
        }
    }

    public void UpdateShaderRadiusValues()
    {
        if (maskMaterial != null)
        {
            maskMaterial.SetFloat("_RadiusX", radiusX + 0.008f);
            maskMaterial.SetFloat("_RadiusY", radiusY + 0.0145f);
        }
    }

    private void Update()
    {
        if (playerHP == null)
        {
            return;
        }

        SaveManager.instance.nowPlayer.playerlightgage_max = maxLightGage;
        SaveManager.instance.nowPlayer.playerlightgage = currentLightGage;

        Vector3 maskPosition = mainCamera.WorldToScreenPoint(player.position) + new Vector3(0, 60, 0);
        maskImage.position = maskPosition;

        if(currentLightGage > 0)
        {
            currentLightGage -= Time.deltaTime * lightreduction;
        }
        else if(currentLightGage <= 0)
        {
            playerHP.TakeDamage(Time.deltaTime * 10);
        }

        currentLightGage = Mathf.Max(currentLightGage, 0);

        float normalizedLightGage = currentLightGage / maxLightGage;
        maskMaterial.SetFloat("_LightGage", normalizedLightGage);

        radiusX = (0.16f / 2.5f * normalizedLightGage);
        radiusY = (0.29f / 2.5f * normalizedLightGage);
        UpdateShaderRadiusValues();

        if (Input.GetKeyDown(KeyCode.X))
        {
            if(playerData.FireWood > 0 && availability == true)
            {
                StartCoroutine(RechargeLightGage());
                playerData.FireWood--;
            }
            else if(playerData.FireWood < 0)
            {
                Debug.Log("장작이 없음");
            }
            else if(availability == false)
            {
                Debug.Log("장작 부식 상태라 회복이 불가능합니다.");
            }
            
        }
    }

    private IEnumerator RechargeLightGage()
    {
        float rechargeSpeed = 50f;
        float targetLightGage = Mathf.Min(currentLightGage + maxLightGage / 2, maxLightGage);

        while (currentLightGage < targetLightGage)
        {
            currentLightGage += rechargeSpeed * Time.deltaTime;
            yield return null;
        }

        currentLightGage = targetLightGage;
    }

}
