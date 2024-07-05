using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private GameObject[] stages;

    private void Awake()
    {
        ActivateRandomStage();
    }

    private void ActivateRandomStage()
    {
        foreach (GameObject stage in stages)
        {
            stage.SetActive(false);
        }

        // 랜덤으로 하나의 스테이지 활성화
        int randomIndex = Random.Range(0, stages.Length);
        stages[randomIndex].SetActive(true);

        Debug.Log((randomIndex + 1) + " 번 스테이지 활성화");
    }
}
