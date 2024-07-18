using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurseSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject curseSelectionPanel;
    [SerializeField] private Button[] curseButtons;
    [SerializeField] private TextMeshProUGUI[] curseNameTexts;
    [SerializeField] private TextMeshProUGUI[] curseDescriptionTexts;


    private Player player;
    private List<Curse> curses;
    public List<Curse> Curses
    {
        get
        {
            return curses;
        }
    }
    private List<Curse> selectedCurses;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player 객체를 찾을 수 없습니다. Player 태그를 확인해주세요.");
            return;
        }

        curses = new List<Curse>
        {
            new Curse(CurseType.장작부식, "장작부식", "횟불 게이지를 베이스에서만 회복할 수 있게 됩니다."),
            new Curse(CurseType.안절부절, "안절부절", "점프키가 비활성화되며 캐릭터가 자동으로 점프합니다."),
            new Curse(CurseType.횟불고장1, "횟불고장(1)", "횟불 게이지가 2배 빠르게 감소합니다."),
            new Curse(CurseType.횟불고장2, "횟불고장(2)", "횟불 게이지의 최대치가 30% 감소합니다."),
            new Curse(CurseType.침침한눈, "침침한눈", "시야 반경이 줄어듭니다."),
            new Curse(CurseType.과유불급, "과유불급", "10초마다 2초간 속도가 매우 빨라집니다."),
            new Curse(CurseType.돈이좋아, "돈이 좋아!", "1분 마다 플레이 도중에 광고가 뜹니다."),
            new Curse(CurseType.망각, "망각", "횟불 게이지를 표시하지 않습니다."),
            new Curse(CurseType.자원고갈, "자원 고갈", "베이스에서 횟불 게이지를 회복하지 못합니다."),
            new Curse(CurseType.상자의저주, "상자의 저주", "보물상자에서 패널티가 나올 확률이 증가하지만 더 좋은 보상을 얻을 수 있습니다.")
        };

        if (curseButtons == null || curseButtons.Length < 3)
        {
            Debug.LogError("curseButtons 배열이 null이거나 길이가 충분하지 않습니다.");
            return;
        }

        for (int i = 0; i < curseButtons.Length; i++)
        {
            int index = i;
            curseButtons[i].onClick.AddListener(() => SelectCurse(index));
        }

        selectedCurses = SelectRandomCurses(curseButtons.Length);
        DisplayCurses(selectedCurses);
    }

    public void ShowCurseOptions()
    {
        if (curseSelectionPanel != null)
        {
            curseSelectionPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("curseSelectionPanel이 설정되지 않았습니다.");
        }
    }

    private void SelectCurse(int curseIndex)
    {
        if (curseIndex < 0 || curseIndex >= curseButtons.Length)
        {
            Debug.LogError("잘못된 저주 인덱스: " + curseIndex);
            return;
        }

        Curse selectedCurse = selectedCurses[curseIndex];
        Debug.Log(selectedCurse.Name + " 선택됨");

        selectedCurse.Apply(player);
        player.activeCurses.Add(selectedCurse);
        player?.Respawn();


        selectedCurses = SelectRandomCurses(curseButtons.Length);
        DisplayCurses(selectedCurses);

        if (curseSelectionPanel != null)
        {
            curseSelectionPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("curseSelectionPanel이 설정되지 않았습니다.");
        }
    }

    private List<Curse> SelectRandomCurses(int count)
    {
        List<Curse> selectedCurses = new List<Curse>();
        List<Curse> cursesList = new List<Curse>(curses);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, cursesList.Count);
            selectedCurses.Add(cursesList[randomIndex]);
            cursesList.RemoveAt(randomIndex);
        }

        return selectedCurses;
    }

    private void DisplayCurses(List<Curse> selectedCurses)
    {
        for (int i = 0; i < selectedCurses.Count; i++)
        {
            curseNameTexts[i].text = selectedCurses[i].Name;
            curseDescriptionTexts[i].text = selectedCurses[i].Description;
        }
    }
}
