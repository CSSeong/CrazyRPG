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
            GameStartUI.SetActive(true); // GameObject를 활성화
        }
        else
        {
            Debug.LogWarning("UI GameObject가 할당되지 않았습니다.");
        }
    }

}
