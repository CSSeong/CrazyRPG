using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 100;
    private float currentHP;

    private SpriteRenderer spriteRenderer;
    private Color originColor;

    public float MaxHP => maxHP;
    public float CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
    }

    public void TakeDamage(float damage)
    {
        currentHP = currentHP - damage > 0 ? currentHP - damage : 0;

        StopCoroutine(nameof(HitAnimation));
        StartCoroutine(nameof(HitAnimation));

        if (currentHP <= 0)
        {
            Debug.Log("플레이어 사망");
        }
    }

    private IEnumerator HitAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originColor;
    }
}
