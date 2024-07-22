using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightBall : MonoBehaviour
{
    [SerializeField]
    private GameObject player;   // 플레이어 오브젝트
    [SerializeField]
    private GameObject lightmask; // UI 오브젝트

    private List<GameObject> lightKits = new List<GameObject>(); // 광원 오브젝트 리스트
    private HashSet<GameObject> startedCoroutines = new HashSet<GameObject>(); // 이미 코루틴이 시작된 오브젝트를 추적
    private float activationDistance = 5.0f; // UI가 비활성화될 거리
    private float lightDuration = 60f;       // 광원의 지속 시간

    private void Update()
    {
        // 씬에서 "LightKit" 태그를 가진 모든 오브젝트를 찾아 리스트 업데이트
        GameObject[] currentLightKits = GameObject.FindGameObjectsWithTag("LightKit");

        foreach (GameObject lightKit in currentLightKits)
        {
            if (!lightKits.Contains(lightKit))
            {
                lightKits.Add(lightKit);

                if (!startedCoroutines.Contains(lightKit))
                {
                    StartCoroutine(DestroyLightAfterTime(lightDuration, lightKit));
                    startedCoroutines.Add(lightKit);
                }
            }
        }

        // 필수 요소 확인
        if (player == null || lightmask == null)
        {
            Debug.LogWarning("player 또는 lightmask가 할당되지 않았습니다.");
            return; // 필수 요소가 없으면 거리 계산 및 UI 업데이트를 수행하지 않음
        }

        bool anyLightKitWithinDistance = false;

        // 모든 lightKit에 대해 거리 계산
        foreach (GameObject lightKit in lightKits)
        {
            if (lightKit == null) continue; // lightKit이 이미 삭제된 경우 건너뜀

            float distance = Vector3.Distance(player.transform.position, lightKit.transform.position);
            Debug.Log($"Distance: {distance} to {lightKit.name}");  // 거리 출력

            if (distance < activationDistance)
            {
                anyLightKitWithinDistance = true;
                break;
            }
        }

        // 하나라도 거리에 들어온 lightKit이 있는 경우 UI 비활성화, 그렇지 않으면 활성화
        lightmask.SetActive(!anyLightKitWithinDistance);
    }

    private IEnumerator DestroyLightAfterTime(float duration, GameObject lightKit)
    {
        yield return new WaitForSeconds(duration);

        if (lightKit != null)
        {
            lightKits.Remove(lightKit); // lightKit이 삭제될 때 리스트에서도 제거
            startedCoroutines.Remove(lightKit); // 코루틴 추적에서 제거
            Destroy(lightKit); // lightKit이 null이 아닐 때만 삭제
        }
    }
}
