using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    [Header("Camera Limit")]
    [SerializeField]
    private float cameraLimitMinX;
    [SerializeField]
    private float cameraLimitMaxX;

    [Header("player Limit")]
    [SerializeField]
    private float playerLimitMinX;
    [SerializeField]
    private float playerLimitMaxX;

    [Header("Map Limit")]
    [SerializeField]
    private float mapLimitMinY;

    public float CameraLimitMinX => cameraLimitMinX;
    public float CameraLimitMaxX => cameraLimitMaxX;
    public float PlayerLimitMinX => playerLimitMinX;
    public float PlayerLimitMaxX => playerLimitMaxX;
    public float MapLimitMinY => mapLimitMinY;

}
