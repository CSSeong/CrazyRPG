using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private float maxHP = 100f;
    private float currentHP;

    private Player player;
    private SpriteRenderer spriteRenderer;
    private Color originColor;

    public float MaxHP
    {
        get => maxHP;
        set => maxHP = value;
    }

    public float CurrentHP
    {
        get => currentHP;
        set
        {
            currentHP = Mathf.Clamp(value, 0, maxHP);
            if (currentHP <= 0 && player.IsAlive)
            {
                player.Die();
            }
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
        player = GetComponentInParent<Player>();

        if (SaveManager.instance != null)
        {
            maxHP = SaveManager.instance.nowPlayer.playerHP_max;
            currentHP = SaveManager.instance.nowPlayer.playerHP;
        }
        else
        {
            currentHP = maxHP;
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage * 5;

        if (!IsInvoking(nameof(HitAnimation)))
        {
            StartCoroutine(HitAnimation());
        }
    }

    private IEnumerator HitAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originColor;
    }
}
