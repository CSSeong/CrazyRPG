using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private PlayerMove playerMove;

    RaycastHit hitInfo;

    private float rayDistance = 3.0f;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }
    private void Update()
    {
        CheckObject();
    }

    private void CheckObject()
    {
        Vector2 playerDirection = playerMove.MoveDirection;

        if (Physics.Raycast(transform.position, playerDirection, out hitInfo, rayDistance))
        {
            Debug.Log("충돌한 오브젝트 이름: " + hitInfo.transform.name);
            // 여기서 필요한 추가 동작을 수행할 수 있습니다.
        }
        else
        {
            Debug.Log("데이터가 없음");
        }
    }

}
