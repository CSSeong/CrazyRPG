using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightBall : MonoBehaviour
{
    [SerializeField]
    private GameObject player;   // �÷��̾� ������Ʈ
    [SerializeField]
    private GameObject lightmask; // UI ������Ʈ

    private List<GameObject> lightKits = new List<GameObject>(); // ���� ������Ʈ ����Ʈ
    private HashSet<GameObject> startedCoroutines = new HashSet<GameObject>(); // �̹� �ڷ�ƾ�� ���۵� ������Ʈ�� ����
    private float activationDistance = 5.0f; // UI�� ��Ȱ��ȭ�� �Ÿ�
    private float lightDuration = 60f;       // ������ ���� �ð�

    private void Update()
    {
        // ������ "LightKit" �±׸� ���� ��� ������Ʈ�� ã�� ����Ʈ ������Ʈ
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

        // �ʼ� ��� Ȯ��
        if (player == null || lightmask == null)
        {
            Debug.LogWarning("player �Ǵ� lightmask�� �Ҵ���� �ʾҽ��ϴ�.");
            return; // �ʼ� ��Ұ� ������ �Ÿ� ��� �� UI ������Ʈ�� �������� ����
        }

        bool anyLightKitWithinDistance = false;

        // ��� lightKit�� ���� �Ÿ� ���
        foreach (GameObject lightKit in lightKits)
        {
            if (lightKit == null) continue; // lightKit�� �̹� ������ ��� �ǳʶ�

            float distance = Vector3.Distance(player.transform.position, lightKit.transform.position);
            Debug.Log($"Distance: {distance} to {lightKit.name}");  // �Ÿ� ���

            if (distance < activationDistance)
            {
                anyLightKitWithinDistance = true;
                break;
            }
        }

        // �ϳ��� �Ÿ��� ���� lightKit�� �ִ� ��� UI ��Ȱ��ȭ, �׷��� ������ Ȱ��ȭ
        lightmask.SetActive(!anyLightKitWithinDistance);
    }

    private IEnumerator DestroyLightAfterTime(float duration, GameObject lightKit)
    {
        yield return new WaitForSeconds(duration);

        if (lightKit != null)
        {
            lightKits.Remove(lightKit); // lightKit�� ������ �� ����Ʈ������ ����
            startedCoroutines.Remove(lightKit); // �ڷ�ƾ �������� ����
            Destroy(lightKit); // lightKit�� null�� �ƴ� ���� ����
        }
    }
}
