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
        //80% �ູ, 19%������, 1%���� ������ Ȯ���� �÷��̾�� �ο�
        gameObject.SetActive(false);
    }
}
