using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private GameObject GameOverPanel;
    [SerializeField]
    private Button[] selectButtons;

    private CurseSelectionUI curseSelectionUI;

    private void Awake()
    {
        curseSelectionUI = FindInactiveObject<CurseSelectionUI>();
        selectButtons[0].onClick.AddListener(() => OnButtonClicked());
        selectButtons[1].onClick.AddListener(() => ReturnIntro());
    }

    public void ShowOptions()
    {
        GameOverPanel.SetActive(true);
    }

    private void OnButtonClicked()
    {
        GameOverPanel.SetActive(false);
        curseSelectionUI.ShowCurseOptions();
    }

    private void ReturnIntro()
    {
        SceneManager.LoadScene("IntroScene");
    }

    private T FindInactiveObject<T>() where T : Component
    {
        foreach (T obj in Resources.FindObjectsOfTypeAll<T>())
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
}
