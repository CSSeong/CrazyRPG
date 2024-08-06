using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [Header("LayerMask")]
    [SerializeField] private LayerMask groundCheckLayer;
    [SerializeField] private LayerMask collisionCheckLayer;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 4.5f;
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    public float JumpForce
    {
        get => jumpForce;
        set => jumpForce = value;
    }

    [SerializeField] private float lowGravityScale = 2f;
    [SerializeField] private float highGravityScale = 3.5f;

    private Vector2 collisionSize;
    private Vector2 footPosition;
    private Vector2 collisionPosition;

    private Rigidbody2D rigid2D;
    private Collider2D myCollider2D;

    public bool IsLongJump { get; set; } = false;
    public bool IsGrounded { get; private set; } = false;
    public Collider2D HitObject { get; private set; }

    public Vector2 Velocity => rigid2D.velocity;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        myCollider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        UpdateCollision();
        UpdateJumpHeight();
    }

    public void SetMoveSpeed(float newMoveSpeed) => MoveSpeed = newMoveSpeed;

    public void SetJumpForce(float newJumpForce) => JumpForce = newJumpForce;

    public void MoveTo(float x) => rigid2D.velocity = new Vector2(x * moveSpeed, rigid2D.velocity.y);

    private void UpdateCollision()
    {
        Bounds bounds = myCollider2D.bounds;

        collisionSize = new Vector2((bounds.max.x - bounds.min.x) * 0.5f, 0.1f);

        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        collisionPosition = bounds.center;

        IsGrounded = Physics2D.OverlapBox(footPosition, collisionSize, 0, groundCheckLayer);
        HitObject = Physics2D.OverlapBox(collisionPosition, new Vector2(bounds.size.x, bounds.size.y), 0, collisionCheckLayer);
    }

    public void jump()
    {
        if (IsGrounded)
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpForce);
        }
    }

    private void UpdateJumpHeight()
    {
        rigid2D.gravityScale = IsLongJump && rigid2D.velocity.y > 0 ? lowGravityScale : highGravityScale;
    }
}
