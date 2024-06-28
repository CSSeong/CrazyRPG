using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private PlayerHP playerHP;
    private PlayerLight playerLight;
    public PlayerLight PlayerLight => playerLight;

    private Vector3 deathPosition;

    private bool isAlive = true;
    public bool IsAlive => isAlive;

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

    private void Awake()
    {
        playerHP = GetComponentInChildren<PlayerHP>();
        playerLight = GetComponent<PlayerLight>();
        gameOverUI = FindInactiveObject<GameOverUI>();
        lightUI = GameObject.FindGameObjectWithTag("LightUI");
        deathCount = 1;
    }

    public void Die()
    {
        Debug.Log("Player is dead.");
        isAlive = false;
        deathPosition = transform.position;
        if (deathCount > 0)
        {
            if (gameOverUI != null)
            {
                gameOverUI.ShowOptions();
                deathCount--;
            }
        }
        else if (deathCount == 0)
        {
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

