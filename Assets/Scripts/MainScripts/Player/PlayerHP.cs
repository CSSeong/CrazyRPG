using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 100;
    private float currentHP;

    private Player player;

    private SpriteRenderer spriteRenderer;
    private Color originColor;

    public float MaxHP
    {
        get { return maxHP; }
        set
        {
            SaveManager.instance.nowPlayer.playerHP_max = maxHP;
        }
    }

    public float CurrentHP
    {
        get { return currentHP; }
        set
        {
            currentHP = Mathf.Clamp(value, 0, maxHP);
            if(currentHP <= 0 && player.IsAlive)
            {
                player.Die();
            }
            SaveManager.instance.nowPlayer.playerHP = currentHP;
        }
    }

    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
        player = GetComponentInParent<Player>();

        if (SaveManager.instance != null)
        {
            currentHP = SaveManager.instance.nowPlayer.playerHP;
            maxHP = SaveManager.instance.nowPlayer.playerHP_max;
        }
    }
    
    public void TakeDamage(float damage)
    {
        
        CurrentHP -= damage * 20; 

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
