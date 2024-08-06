using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private PlayerHP playerHP;
    public PlayerHP PlayerHP
    {
        get
        {
            return playerHP;
        }
    }
    private PlayerLight playerLight;
    public PlayerLight PlayerLight
    {
        get
        {
            return playerLight;
        }
    }
    private Movement2D movement;
    public Movement2D Movement2D => movement;

    private Vector3 deathPosition;

    private bool isAlive = true;
    public bool IsAlive => isAlive;

    private bool isBlessing = false;
    public bool IsBlessing
    {
        get => isBlessing;
        set => isBlessing = value;
    }

    private GameOverUI gameOverUI;
    private int deathCount;

    private GameObject lightUI;
    public GameObject LightUI => lightUI;

    private bool isJumpDisabled = false;
    public bool IsJumpDisabled
    {
        get { return isJumpDisabled; }
        set { isJumpDisabled = value; }
    }

    private bool isRunFastEnabled = false;
    public bool IsRunFastEnabled
    {
        get { return isRunFastEnabled; }
        set { isRunFastEnabled = value; }
    }

    public List<Curse> activeCurses = new List<Curse>();

    private void Awake()
    {
        playerHP = GetComponentInChildren<PlayerHP>();
        playerLight = GetComponent<PlayerLight>();
        movement = GetComponent<Movement2D>();
        gameOverUI = FindInactiveObject<GameOverUI>();
        lightUI = GameObject.FindGameObjectWithTag("LightUI");
        deathCount = 1;
    }

    private void Start()
    {
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        if (SaveManager.instance == null) return;

        movement.SetMoveSpeed(SaveManager.instance.nowPlayer.moveSpeed);
        movement.SetJumpForce(SaveManager.instance.nowPlayer.jumpForce);
        playerHP.MaxHP = SaveManager.instance.nowPlayer.playerHP_max;
        playerHP.CurrentHP = SaveManager.instance.nowPlayer.playerHP;
        playerLight.MaxLightGage = SaveManager.instance.nowPlayer.playerlightgage_max;
        playerLight.CurrentLightGage = SaveManager.instance.nowPlayer.playerlightgage;
    }

    public void Die()
    {
        Debug.Log("Player is dead.");
        isAlive = false;
        deathPosition = transform.position;

        if (deathCount > 0)
        {
            if (gameOverUI != null && !IsBlessing)
            {
                gameOverUI.ShowOptions();
                deathCount--;
            }
            else
            {
                Debug.Log("'[축복]부활의 가호'효과로 저주를 받지 않고 되살아납니다.");
                deathCount--;
                Respawn();
            }
        }
        else if (deathCount == 0)
        {
            SaveManager.instance.DeleteData();
            SceneManager.LoadScene("IntroScene");
        }
    }

    public void Respawn()
    {
        if (playerHP != null)
        {
            playerHP.CurrentHP = playerHP.MaxHP;
        }
        else
        {
            Debug.LogError("PlayerHP 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        playerLight.CurrentLightGage = playerLight.MaxLightGage;
        isAlive = true;
        transform.position = deathPosition;

        SavePlayerData();
    }

    private void SavePlayerData()
    {
        if (SaveManager.instance == null) return;

        SaveManager.instance.nowPlayer.playerHP = playerHP.CurrentHP;
        SaveManager.instance.nowPlayer.playerHP_max = playerHP.MaxHP;
        SaveManager.instance.nowPlayer.playerlightgage = playerLight.CurrentLightGage;
        SaveManager.instance.nowPlayer.playerlightgage_max = playerLight.MaxLightGage;
        SaveManager.instance.nowPlayer.moveSpeed = movement.MoveSpeed;
        SaveManager.instance.nowPlayer.jumpForce = movement.JumpForce;

        SaveManager.instance.SaveData();
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

