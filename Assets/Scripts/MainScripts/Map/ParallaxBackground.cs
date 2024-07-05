using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BackgroundData
{
    public Renderer background;
    public float speed;
}

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    private Transform targetCamera;
    [SerializeField]
    private BackgroundData[] backgrounds;

    private float targetStartX;

    private void Awake()
    {
        targetStartX = targetCamera.position.x;
    }

    private void Update()
    {
        if (targetCamera == null) return;

        float x = targetCamera.position.x - targetStartX;
        for(int i = 0; i < backgrounds.Length; ++i)
        {
            float offset = x * backgrounds[i].speed;
            backgrounds[i].background.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}
