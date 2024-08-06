using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLight : MonoBehaviour
{
    [SerializeField] private RectTransform maskImage;
    public RectTransform MaskImage
    {
        get => maskImage;
        set => maskImage = value;
    }
    [SerializeField] private Transform player;
    [SerializeField] private Camera mainCamera;

    private float maxLightGage = 100f;
    private float currentLightGage;
    private PlayerHP playerHP;
    private DialogueManager dialogueManager;

    private float radiusX;
    public float RadiusX
    {
        get => radiusX;
        set => radiusX = value;
    }
    private float radiusY;
    public float RadiusY
    {
        get => radiusY;
        set => radiusY = value;
    }

    public float MaxLightGage
    {
        get => maxLightGage;
        set => maxLightGage = value;
    }

    public float CurrentLightGage
    {
        get => currentLightGage;
        set => currentLightGage = Mathf.Clamp(value, 0, maxLightGage);
    }

    private float lightReduction = 4.0f;
    public float Lightreduction
    {
        get => lightReduction;
        set => lightReduction = value;
    }

    private Material maskMaterial;
    private bool availability = true;
    public bool Availability
    {
        get => availability;
        set => availability = value;
    }

    private void Awake()
    {
        if (SaveManager.instance != null)
        {
            maxLightGage = SaveManager.instance.nowPlayer.playerlightgage_max;
            currentLightGage = SaveManager.instance.nowPlayer.playerlightgage;
        }
        else
        {
            currentLightGage = maxLightGage;
        }

        maskMaterial = maskImage.GetComponent<Image>().material;
        dialogueManager = FindObjectOfType<DialogueManager>();
        playerHP = FindObjectOfType<PlayerHP>();
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
        if (playerHP == null) return;

        Vector3 maskPosition = mainCamera.WorldToScreenPoint(player.position) + new Vector3(0, 60, 0);
        maskImage.position = maskPosition;

        if (!dialogueManager.IsDialogue && !BlessingManager.instance.BlessingSelection.gameObject.activeSelf &&
            !BlessingManager.instance.CurseSelection.gameObject.activeSelf)
        {
            if (currentLightGage > 0)
            {
                currentLightGage -= Time.deltaTime * lightReduction;
            }
            else
            {
                playerHP.TakeDamage(Time.deltaTime * 5);
            }
        }

        currentLightGage = Mathf.Max(currentLightGage, 0);

        float normalizedLightGage = currentLightGage / maxLightGage;
        maskMaterial.SetFloat("_LightGage", normalizedLightGage);

        radiusX = (0.16f / 2.5f * normalizedLightGage);
        radiusY = (0.29f / 2.5f * normalizedLightGage);
        UpdateShaderRadiusValues();
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

    public void Recharge() => StartCoroutine(RechargeLightGage());
}
