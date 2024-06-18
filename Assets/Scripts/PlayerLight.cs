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

    private void Update()
    {
        Vector3 maskPosition = mainCamera.WorldToScreenPoint(player.position);
        maskImage.position = maskPosition;
    }
}
