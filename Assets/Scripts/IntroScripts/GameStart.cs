using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    private GameObject GameStartUI;

    public void ActivateUI()
    {
        if (GameStartUI != null)
        {
            GameStartUI.SetActive(true); // GameObject�� Ȱ��ȭ
        }
        else
        {
            Debug.LogWarning("UI GameObject�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

}
