using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    [Header("스킬 슬롯 UI")]
    [SerializeField]
    private SkillSlot skillSlot;

    [Header("아이템 사용을 위해 참조해야 할 것들")]
    [SerializeField]
    private PlayerLight playerlight;
    public PlayerLight _playerLight
    {
        get
        {
            return playerlight;
        }
    }
    [SerializeField]
    private PlayerHP playerHP;
    public PlayerHP _playerHP
    {
        get
        {
            return playerHP;
        }
    }
    [SerializeField]
    private DialogueManager dialogueManager;
    public DialogueManager _dialogueManager
    {
        get
        {
            return dialogueManager;
        }
    }
    [SerializeField]
    private GameObject lightPrefab;
    public GameObject LightPrefab
    {
        get
        {
            return lightPrefab;
        }
    }
    [SerializeField]
    private Transform player;
    public Transform Player
    {
        get
        {
            return player;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void AddSkill(Item item)
    {
        if (skillSlot.SkillItem == null)
        {
            int itemCount = InventoryMain.Instance.GetItemCount(item);
            skillSlot.AddSkill(item, itemCount);
            return;
        }
        Debug.Log("스킬 슬롯이 가득 찼습니다.");
    }

    public void RemoveSkill()
    {
        if (skillSlot.SkillItem == null)
        {
            Debug.Log("스킬 슬롯이 비어있습니다.");
        }
        else
        {
            skillSlot.ClearSlot();
            return;
        }
    }

    public void OnSkillSlotClicked()
    {
        SkillSlot clickedSlot = skillSlot;

        if (clickedSlot.SkillItem != null)
        {
            clickedSlot.UseSkill();
        }
    }

    public bool IsSkillSlotEmpty()
    {
        return skillSlot.SkillItem == null;
    }

    public SkillSlot GetSkillSlot()
    {
        return skillSlot;
    }
}