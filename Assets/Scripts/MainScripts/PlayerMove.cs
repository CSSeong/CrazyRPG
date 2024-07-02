using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private KeyCode jumpKeyCode = KeyCode.Space;

    private Player player;

    private Movement2D movement;
    private PlayerAnimator playerAnimator;

    private Direction direction = Direction.Right;

    private Coroutine autoJumpCoroutine;
    private float autoJumpInterval = 0.5f;

    private Coroutine speedBoostCoroutine;
    private float originalMoveSpeed;

    private Vector2 moveDirection = Vector2.right;
    public Vector2 MoveDirection => moveDirection;

    private void Awake()
    {
        movement = GetComponent<Movement2D>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        player = GetComponent<Player>();

        // 원래 속도를 저장
        originalMoveSpeed = movement.MoveSpeed;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Direction.Right;
        }

        if (x < 0)
        { 
            moveDirection = Vector2.left;
        }
        else if (x > 0)
        {
            moveDirection = Vector2.right;
        }

        UpdateMove(x);
        UpdateJump();

        playerAnimator.UpdateAnimation(x);

        UpdateCheckCollision();

    }

    private void UpdateMove(float x)
    {
        if (!player.IsRunFastEnabled)
        {
            movement.MoveTo(x);
        }
        else
        {
            if (speedBoostCoroutine == null)
            {
                speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine());
            }
            movement.MoveTo(x);
        }

        float xPosition = Mathf.Clamp(transform.position.x, stageData.PlayerLimitMinX, stageData.PlayerLimitMaxX);
        transform.position = new Vector2(xPosition, transform.position.y);
        transform.localEulerAngles = new Vector3(0, (int)direction, 0);
    }

    private void UpdateJump()
    {
        if (!player.IsJumpDisabled)
        {
            if (Input.GetKeyDown(jumpKeyCode))
            {
                movement.jump();
            }

            if (Input.GetKey(jumpKeyCode))
            {
                movement.IsLongJump = true;
            }
            else if (Input.GetKeyUp(jumpKeyCode))
            {
                movement.IsLongJump = false;
            }
        }
        else
        {
            if (autoJumpCoroutine == null)
            {
                autoJumpCoroutine = StartCoroutine(AutoJumpRoutine());
            }
        }
    }

    private void UpdateCheckCollision()
    {
        if(movement.HitObject != null)
        {
            if (movement.HitObject.TryGetComponent<Boxbase>(out var box))
            {
                box.UpdateCollision();
            }
        }
        
    }

    private IEnumerator AutoJumpRoutine()
    {
        while (player.IsJumpDisabled)
        {
            yield return new WaitForSeconds(autoJumpInterval);
            movement.IsLongJump = true;
            movement.jump();
        }
        autoJumpCoroutine = null;
    }

    private IEnumerator SpeedBoostRoutine()
    {
        movement.MoveSpeed *= 10.0f; // 속도를 10배로 증가
        yield return new WaitForSeconds(2.0f);
        movement.MoveSpeed = originalMoveSpeed; // 속도를 원래대로 복원
        yield return new WaitForSeconds(10.0f);
        speedBoostCoroutine = null;
    }

}
