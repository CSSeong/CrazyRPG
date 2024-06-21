using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private PlayerHP playerHP;
    private PlayerLight playerLight;

    private Vector3 deathPosition;

    private bool isAlive = true;
    public bool IsAlive => isAlive;

    private GameOverUI gameOverUI;

    private void Awake()
    {
        playerHP = GetComponentInChildren<PlayerHP>();
        playerLight = GetComponent<PlayerLight>();
        gameOverUI = FindInactiveObject<GameOverUI>();
    }

    public void Die()
    {
        Debug.Log("Player is dead.");
        isAlive = false;
        deathPosition = transform.position;
        if (gameOverUI != null)
        {
            gameOverUI.ShowOptions();
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
            Debug.LogError("PlayerHP ������Ʈ�� ã�� �� �����ϴ�.");
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

