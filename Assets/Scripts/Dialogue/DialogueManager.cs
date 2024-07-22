using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject go_DialogueBar;
    [SerializeField]
    private TextMeshProUGUI text_dialogue;
    [SerializeField]
    private TextMeshProUGUI text_name;

    Dialogue[] dialogues;
    InteractionController theIC;

    private bool isDialogue = false;
    public bool IsDialogue => isDialogue;
    private bool isNext = false;

    [Header("텍스트 출력 딜레이.")]
    [SerializeField]
    private float textDelay;

    private int lineCount = 0;
    private int contextCount = 0;

    private bool isUse = false;
    public bool IsUse
    {
        get
        {
            return isUse;
        }
    }
    private bool hasPower = false; // 전원 상태 체크

    private void Awake()
    {
        theIC = FindObjectOfType<InteractionController>();
    }

    private void Update()
    {
        if (isDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!hasPower)
                {
                    EndDialogue();
                }
                else
                {
                    // 전원이 있는 상태에서 대화 진행
                    if (isNext)
                    {
                        isNext = false;
                        text_dialogue.text = "";
                        if (++contextCount < dialogues[lineCount].contexts.Length)
                        {
                            StartCoroutine(TypeWriter());
                        }
                        else
                        {
                            contextCount = 0;
                            if (++lineCount < dialogues.Length)
                            {
                                StartCoroutine(TypeWriter());
                            }
                            else
                            {
                                EndDialogue();
                                hasPower = false;
                            }
                        }
                    }
                }
            }
        }
    }

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        if (!hasPower)
        {
            isDialogue = true;
            text_name.text = "단말기";
            text_dialogue.text = InventoryMain.Instance.HasItem(2) ? "던전 바깥에 구조 요청을 할 수 있는 장치이다. 배터리를 사용해 구조요청을 하자" : "던전 바깥에 구조 요청을 할 수 있는 장치이다. 배터리가 있으면 작동 시킬 수 있을 것 같다.";
            theIC.SettingUI(false);
            SettingUI(true);
            isUse = true;
            return;
        }
        else
        {
            isUse = false;
            isDialogue = true;
            text_name.text = "";
            text_dialogue.text = "";
            theIC.SettingUI(false);
            dialogues = p_dialogues;
            StartCoroutine(TypeWriter());
        }
    }

    public void UseBattery()
    {
        if (InventoryMain.Instance.HasItem(2))
        {
            hasPower = true;
            Debug.Log("배터리가 사용되어 전원이 들어왔습니다.");

        }
        else
        {
            Debug.Log("배터리가 없습니다.");
        }
    }

    public void EndDialogue()
    {
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        theIC.SettingUI(true);
        SettingUI(false);
    }

    private void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
    }

    private IEnumerator TypeWriter()
    {
        SettingUI(true);
        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");

        
        text_name.text = dialogues[lineCount].name;
        for(int i = 0; i < t_ReplaceText.Length; i++)
        {
            text_dialogue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }
        isNext = true;
        
    }
}
