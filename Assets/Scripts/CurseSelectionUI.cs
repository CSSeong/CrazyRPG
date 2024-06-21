using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurseSelectionUI : MonoBehaviour
{
    [SerializeField]
    private GameObject curseSelectionPanel;
    [SerializeField]
    private Button[] curseButtons;
  
    private Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        curseButtons[0].onClick.AddListener(() => SelectCurse(1));
        curseButtons[1].onClick.AddListener(() => SelectCurse(2));
        curseButtons[2].onClick.AddListener(() => SelectCurse(3));
    }

    public void ShowCurseOptions()
    {
        curseSelectionPanel.SetActive(true);
    }

    private void SelectCurse(int curseIndex)
    {
        switch (curseIndex)
        {
            case 1:
                Debug.Log("1번 저주 선택");
                break;
            case 2:
                Debug.Log("2번 저주 선택");
                break;
            case 3:
                Debug.Log("3번 저주 선택");
                break;
        }

        player.Respawn();
        curseSelectionPanel.SetActive(false);
    }
}
