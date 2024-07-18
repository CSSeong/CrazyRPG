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
            Debug.LogError("Player 객체를 찾을 수 없습니다. Player 태그를 확인해주세요.");
            return;
        }

        Blessings = new List<Blessing>
        {
            new Blessing(BlessingType.부활의가호, "부활의 가호", "죽을 때 저주를 받지 않습니다."),
            new Blessing(BlessingType.화력보존, "화력 보존", "초당 횟불 게이지 감소 수치가 15% 감소합니다."),
            new Blessing(BlessingType.넉넉한뗄감, "넉넉한 뗄감", "횟불 게이지 최대치가 20% 증가합니다."),
            new Blessing(BlessingType.매의눈, "매의 눈", "시야 반경이 증가합니다."),
            new Blessing(BlessingType.돈벌어돈, "돈벌어 돈!", "상자 오픈시 얻는 골드가 30% 증가합니다"),
            new Blessing(BlessingType.해주, "해주", "임의의 저주 하나를 제거합니다."),
            new Blessing(BlessingType.화력보급, "화력 보급", "베이스에서 충전되는 화력의 양이 증가합니다."),
            new Blessing(BlessingType.모아니면도, "모 아니면 도", "랜덤 저주 하나를 얻고 랜덤 축복을 2개 얻습니다."),
            new Blessing(BlessingType.저주척결, "저주 척결", "상자에서 저주가 나올 확률이 감소합니다."),
            new Blessing(BlessingType.넘치는체력, "넘치는 체력", "최대 HP가 30% 증가합니다.")
        };

        if (BlessingButtons == null || BlessingButtons.Length < 3)
        {
            Debug.LogError("BlessingButtons 배열이 null이거나 길이가 충분하지 않습니다.");
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
            Debug.LogError("BlessingSelectionPanel이 설정되지 않았습니다.");
        }
    }

    private void SelectBlessing(int BlessingIndex)
    {
        if (BlessingIndex < 0 || BlessingIndex >= BlessingButtons.Length)
        {
            Debug.LogError("잘못된 축복 인덱스: " + BlessingIndex);
            return;
        }

        Blessing selectedBlessing = selectedBlessings[BlessingIndex];
        Debug.Log(selectedBlessing.Name + " 선택됨");

        selectedBlessing.Apply(player);
        if(selectedBlessing.Name == "부활의 가호")
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
            Debug.LogError("BlessingSelectionPanel이 설정되지 않았습니다.");
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
