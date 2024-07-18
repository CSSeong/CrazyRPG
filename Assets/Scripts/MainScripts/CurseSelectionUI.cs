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
            Debug.LogError("Player ��ü�� ã�� �� �����ϴ�. Player �±׸� Ȯ�����ּ���.");
            return;
        }

        curses = new List<Curse>
        {
            new Curse(CurseType.���ۺν�, "���ۺν�", "Ƚ�� �������� ���̽������� ȸ���� �� �ְ� �˴ϴ�."),
            new Curse(CurseType.��������, "��������", "����Ű�� ��Ȱ��ȭ�Ǹ� ĳ���Ͱ� �ڵ����� �����մϴ�."),
            new Curse(CurseType.Ƚ�Ұ���1, "Ƚ�Ұ���(1)", "Ƚ�� �������� 2�� ������ �����մϴ�."),
            new Curse(CurseType.Ƚ�Ұ���2, "Ƚ�Ұ���(2)", "Ƚ�� �������� �ִ�ġ�� 30% �����մϴ�."),
            new Curse(CurseType.ħħ�Ѵ�, "ħħ�Ѵ�", "�þ� �ݰ��� �پ��ϴ�."),
            new Curse(CurseType.�����ұ�, "�����ұ�", "10�ʸ��� 2�ʰ� �ӵ��� �ſ� �������ϴ�."),
            new Curse(CurseType.��������, "���� ����!", "1�� ���� �÷��� ���߿� ���� ��ϴ�."),
            new Curse(CurseType.����, "����", "Ƚ�� �������� ǥ������ �ʽ��ϴ�."),
            new Curse(CurseType.�ڿ���, "�ڿ� ��", "���̽����� Ƚ�� �������� ȸ������ ���մϴ�."),
            new Curse(CurseType.����������, "������ ����", "�������ڿ��� �г�Ƽ�� ���� Ȯ���� ���������� �� ���� ������ ���� �� �ֽ��ϴ�.")
        };

        if (curseButtons == null || curseButtons.Length < 3)
        {
            Debug.LogError("curseButtons �迭�� null�̰ų� ���̰� ������� �ʽ��ϴ�.");
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
            Debug.LogError("curseSelectionPanel�� �������� �ʾҽ��ϴ�.");
        }
    }

    private void SelectCurse(int curseIndex)
    {
        if (curseIndex < 0 || curseIndex >= curseButtons.Length)
        {
            Debug.LogError("�߸��� ���� �ε���: " + curseIndex);
            return;
        }

        Curse selectedCurse = selectedCurses[curseIndex];
        Debug.Log(selectedCurse.Name + " ���õ�");

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
            Debug.LogError("curseSelectionPanel�� �������� �ʾҽ��ϴ�.");
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
