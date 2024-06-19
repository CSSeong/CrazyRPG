using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [Header("LayerMask")]
    [SerializeField]
    private LayerMask groundCheckLayer;

    [Header("Move")]
    [SerializeField]
    private float moveSpeed = 0;

    [Header("Jump")]
    [SerializeField]
    private float jumpForce = 10;
    [SerializeField]
    private float lowGravityScale = 2;
    [SerializeField]
    private float highGravityScale = 3.5f;

    private Vector2 collisionSize;
    private Vector2 footPosition;

    private Rigidbody2D rigid2D;
    private Collider2D collider2D;

    public bool IsLongJump { set; get; } = false;
    public bool IsGrounded { private set; get; } = false;

    public Vector2 Velocity => rigid2D.velocity;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        UpdateCollision();
        JumpHeight();
    }

    public void MoveTo(float x)
    {
        rigid2D.velocity = new Vector2(x * moveSpeed, rigid2D.velocity.y);
    }

    private void UpdateCollision()
    {
        Bounds bounds = collider2D.bounds;

        collisionSize = new Vector2((bounds.max.x - bounds.min.x) * 0.5f, 0.1f);

        footPosition = new Vector2(bounds.center.x, bounds.min.y);

        IsGrounded = Physics2D.OverlapBox(footPosition, collisionSize, 0, groundCheckLayer);
    }

    public void jump()
    {
        if(IsGrounded == true)
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpForce);
        }
    }

    private void JumpHeight()
    {
        if(IsLongJump && rigid2D.velocity.y > 0)
        {
            rigid2D.gravityScale = lowGravityScale;
        }
        else
        {
            rigid2D.gravityScale = highGravityScale;
        }
    }

   
}
