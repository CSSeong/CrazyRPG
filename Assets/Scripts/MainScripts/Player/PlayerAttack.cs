using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab; // 프리팹 변수 이름을 명확히 수정
    [SerializeField]
    private Transform firePoint; // 발사 위치 변수 이름을 명확히 수정

    private float cooltime = 0.3f;
    private float curtime;

    private PlayerMove playerMove;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab is not assigned!");
        }
        if (firePoint == null)
        {
            Debug.LogError("Fire Point is not assigned!");
        }
    }

    private void Update()
    {
        if (curtime <= 0)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                ShootProjectile();
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime; // 쿨타임 감소
    }

    private void ShootProjectile()
    {
        // 발사체를 생성하고 인스턴스를 변수에 저장합니다.
        GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // 생성된 발사체의 projectile 스크립트를 가져와서 방향을 설정합니다.
        projectileInstance.GetComponent<projectile>().SetDirection(playerMove.Direction);
    }
}
