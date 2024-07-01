using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class light_enforce : MonoBehaviour
{
    private PlayerLight playerLight;
    private bool clicktimes = true;

    private void Awake()
    {
        playerLight = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLight>();
    }

    public void Upgradelightgage()
    {
        if (clicktimes)
        {
            playerLight.MaxLightGage *= 1.2f;
            Debug.Log("횟불 게이지 25%상승");
            clicktimes = false;
        }
        else
        {
            Debug.Log("이미 강화 했습니다.");
        }
        
    }
}
