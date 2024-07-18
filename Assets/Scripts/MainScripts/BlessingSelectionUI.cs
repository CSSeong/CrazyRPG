using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlessingSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject BlessingSelectionPanel;
    [SerializeField] private Button[] BlessingButtons;
    [SerializeField] private TextMeshProUGUI[] BlessingNameTexts;
    [SerializeField] private TextMeshProUGUI[] BlessingDescriptionTexts;


    private Player player;
    private List<Blessing> Blessings;
    public List<Blessing> _Blessings
    {
        get
        {
            return Blessings;
        }
    }
    private List<Blessing> selectedBlessings;

    [SerializeField]
    private CurseSelectionUI curseSelectionUI;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player ��ü�� ã�� �� �����ϴ�. Player �±׸� Ȯ�����ּ���.");
            return;
        }

        Blessings = new List<Blessing>
        {
            new Blessing(BlessingType.��Ȱ�ǰ�ȣ, "��Ȱ�� ��ȣ", "���� �� ���ָ� ���� �ʽ��ϴ�."),
            new Blessing(BlessingType.ȭ�º���, "ȭ�� ����", "�ʴ� Ƚ�� ������ ���� ��ġ�� 15% �����մϴ�."),
            new Blessing(BlessingType.�˳��Ѷ���, "�˳��� ����", "Ƚ�� ������ �ִ�ġ�� 20% �����մϴ�."),
            new Blessing(BlessingType.���Ǵ�, "���� ��", "�þ� �ݰ��� �����մϴ�."),
            new Blessing(BlessingType.�����, "������ ��!", "���� ���½� ��� ��尡 30% �����մϴ�"),
            new Blessing(BlessingType.����, "����", "������ ���� �ϳ��� �����մϴ�."),
            new Blessing(BlessingType.ȭ�º���, "ȭ�� ����", "���̽����� �����Ǵ� ȭ���� ���� �����մϴ�."),
            new Blessing(BlessingType.��ƴϸ鵵, "�� �ƴϸ� ��", "���� ���� �ϳ��� ��� ���� �ູ�� 2�� ����ϴ�."),
            new Blessing(BlessingType.����ô��, "���� ô��", "���ڿ��� ���ְ� ���� Ȯ���� �����մϴ�."),
            new Blessing(BlessingType.��ġ��ü��, "��ġ�� ü��", "�ִ� HP�� 30% �����մϴ�.")
        };

        if (BlessingButtons == null || BlessingButtons.Length < 3)
        {
            Debug.LogError("BlessingButtons �迭�� null�̰ų� ���̰� ������� �ʽ��ϴ�.");
            return;
        }

        for (int i = 0; i < BlessingButtons.Length; i++)
        {
            int index = i;
            BlessingButtons[i].onClick.AddListener(() => SelectBlessing(index));
        }

        selectedBlessings = SelectRandomBlessings(BlessingButtons.Length);
        DisplayBlessings(selectedBlessings);
    }

    public void ShowBlessingOptions()
    {
        if (BlessingSelectionPanel != null)
        {
            BlessingSelectionPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("BlessingSelectionPanel�� �������� �ʾҽ��ϴ�.");
        }
    }

    private void SelectBlessing(int BlessingIndex)
    {
        if (BlessingIndex < 0 || BlessingIndex >= BlessingButtons.Length)
        {
            Debug.LogError("�߸��� �ູ �ε���: " + BlessingIndex);
            return;
        }

        Blessing selectedBlessing = selectedBlessings[BlessingIndex];
        Debug.Log(selectedBlessing.Name + " ���õ�");

        selectedBlessing.Apply(player);
        if(selectedBlessing.Name == "��Ȱ�� ��ȣ")
        {
            Blessings.Remove(selectedBlessing);
        }


        selectedBlessings = SelectRandomBlessings(BlessingButtons.Length);
        DisplayBlessings(selectedBlessings);

        if (BlessingSelectionPanel != null)
        {
            BlessingSelectionPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("BlessingSelectionPanel�� �������� �ʾҽ��ϴ�.");
        }

        if (curseSelectionUI.gameObject.activeSelf)
        {
            curseSelectionUI.gameObject.SetActive(false);
        }
    }

    private List<Blessing> SelectRandomBlessings(int count)
    {
        List<Blessing> selectedBlessings = new List<Blessing>();
        List<Blessing> BlessingsList = new List<Blessing>(Blessings);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, BlessingsList.Count);
            selectedBlessings.Add(BlessingsList[randomIndex]);
            BlessingsList.RemoveAt(randomIndex);
        }

        return selectedBlessings;
    }

    private void DisplayBlessings(List<Blessing> selectedBlessings)
    {
        for (int i = 0; i < selectedBlessings.Count; i++)
        {
            BlessingNameTexts[i].text = selectedBlessings[i].Name;
            BlessingDescriptionTexts[i].text = selectedBlessings[i].Description;
        }
    }
}
