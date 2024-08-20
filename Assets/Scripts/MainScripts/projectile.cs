using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private float speed = 7f;
    private Rigidbody2D rb;
    private Vector2 direction = Vector2.right;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // 발사체의 방향과 속도를 설정합니다.
        rb.velocity = direction * speed; // 발사체의 속도 설정
        Destroy(gameObject, 1f); // 1초 후에 발사체를 파괴합니다.
    }

    public void SetDirection(Direction playerDirection)
    {
        // 플레이어 방향에 따라 발사체의 방향을 설정합니다.
        if (playerDirection == Direction.Right)
        {
            direction = Quaternion.Euler(0, 0, 35) * Vector2.right; // 45도 오른쪽으로 발사
        }
        else
        {
            direction = Quaternion.Euler(0, 0, -35) * Vector2.left; // 45도 왼쪽으로 발사
        }
    }
}
