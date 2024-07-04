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

    private bool isDialogue = false;

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        text_name.text = "";
        text_dialogue.text = "";

        dialogues = p_dialogues;
        SettingUI(true);
    }

    public void HideDialogue()
    {
        text_name.text = "";
        text_dialogue.text = "";

        SettingUI(false);
    }

    private void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
    }
}
