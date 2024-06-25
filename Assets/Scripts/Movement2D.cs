using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [Header("LayerMask")]
    [SerializeField]
    private LayerMask groundCheckLayer;
    [SerializeField]
    private LayerMask CollisionCheckLayer;


    [Header("Move")]
    [SerializeField]
    private float moveSpeed = 0;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    [Header("Jump")]
    [SerializeField]
    private float jumpForce = 10;
    [SerializeField]
    private float lowGravityScale = 2;
    [SerializeField]
    private float highGravityScale = 3.5f;

    private Vector2 collisionSize;
    private Vector2 footPosition;
    private Vector2 collisionPosition;

    private Rigidbody2D rigid2D;
    private Collider2D mycollider2D;

    public bool IsLongJump { set; get; } = false;
    public bool IsGrounded { private set; get; } = false;
    public Collider2D HitObject { private set; get; }

    public Vector2 Velocity => rigid2D.velocity;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        mycollider2D = GetComponent<Collider2D>();
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
        Bounds bounds = mycollider2D.bounds;

        collisionSize = new Vector2((bounds.max.x - bounds.min.x) * 0.5f, 0.1f);

        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        collisionPosition = bounds.center;

        IsGrounded = Physics2D.OverlapBox(footPosition, collisionSize, 0, groundCheckLayer);
        HitObject = Physics2D.OverlapBox(collisionPosition, new Vector2(bounds.size.x, bounds.size.y), 0, CollisionCheckLayer);
    }

    public void jump()
    {
        if (IsGrounded == true)
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpForce);
        }
    }

    private void JumpHeight()
    {
        if (IsLongJump && rigid2D.velocity.y > 0)
        {
            rigid2D.gravityScale = lowGravityScale;
        }
        else
        {
            rigid2D.gravityScale = highGravityScale;
        }
    }
}
