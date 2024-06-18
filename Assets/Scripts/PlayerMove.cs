using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private KeyCode jumpKeyCode = KeyCode.Space;

    private Movement2D movement;

    private Direction direction = Direction.Right;

    private void Awake()
    {
        movement = GetComponent<Movement2D>();
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
        if(Input.GetKeyDown(jumpKeyCode))
        {
            movement.jump();
        }

        if(Input.GetKey(jumpKeyCode))
        {
            movement.IsLongJump = true;
        }
        else if(Input.GetKeyUp(jumpKeyCode))
        {
            movement.IsLongJump = false;
        }
    }
}
