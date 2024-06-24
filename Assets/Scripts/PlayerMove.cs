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

    private void Awake()
    {
        movement = GetComponent<Movement2D>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Direction.Left;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Direction.Right;
        }

        UpdateMove(x);
        UpdateJump();

        playerAnimator.UpdateAnimation(x);
    }

    private void UpdateMove(float x)
    {
        movement.MoveTo(x);

        float xPosition = Mathf.Clamp(transform.position.x, stageData.PlayerLimitMinX, stageData.PlayerLimitMaxX);
        transform.position = new Vector2(xPosition, transform.position.y);

        transform.localEulerAngles = new Vector3(0, (int)direction, 0);
    }

    private void UpdateJump()
    {
        if (!player.IsJumpDisabled) // 플레이어에게 점프 금지 상태가 아닌 경우만 점프 입력을 받도록 조건 추가
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

    private IEnumerator AutoJumpRoutine()
    {
        while (player.IsJumpDisabled)
        {
            yield return new WaitForSeconds(autoJumpInterval);
            movement.IsLongJump = true;
            movement.jump(); // 자동으로 점프 실행
        }
        autoJumpCoroutine = null; // Coroutine 종료 후 null로 설정
    }
}
