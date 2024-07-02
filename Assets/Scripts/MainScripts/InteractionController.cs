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
            Debug.Log("�浹�� ������Ʈ �̸�: " + hitInfo.transform.name);
            // ���⼭ �ʿ��� �߰� ������ ������ �� �ֽ��ϴ�.
        }
        else
        {
            Debug.Log("�����Ͱ� ����");
        }
    }

}
