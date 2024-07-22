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

    [Header("�ؽ�Ʈ ��� ������.")]
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
    private bool hasPower = false; // ���� ���� üũ

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
                    // ������ �ִ� ���¿��� ��ȭ ����
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
            text_name.text = "�ܸ���";
            text_dialogue.text = InventoryMain.Instance.HasItem(2) ? "���� �ٱ��� ���� ��û�� �� �� �ִ� ��ġ�̴�. ���͸��� ����� ������û�� ����" : "���� �ٱ��� ���� ��û�� �� �� �ִ� ��ġ�̴�. ���͸��� ������ �۵� ��ų �� ���� �� ����.";
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
            Debug.Log("���͸��� ���Ǿ� ������ ���Խ��ϴ�.");

        }
        else
        {
            Debug.Log("���͸��� �����ϴ�.");
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
