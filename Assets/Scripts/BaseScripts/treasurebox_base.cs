using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class treasurebox_base : MonoBehaviour
{
    private PlayerData playerData;

    private void Awake()
    {
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
    }

    public void ClickBox()
    {
        playerData.Coin += 150;
        //80% 축복, 19%아이템, 1%저주 각각의 확률로 플레이어에게 부여
        gameObject.SetActive(false);
    }
}
