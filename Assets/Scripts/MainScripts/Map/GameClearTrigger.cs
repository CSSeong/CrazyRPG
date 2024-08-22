using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 태그가 붙은 오브젝트와 충돌 시
        if (other.CompareTag("Player"))
        {
            // SaveManager 인스턴스를 가져와서 게임 클리어 카운트 증가
            SaveManager.instance.IncrementGameClearCount();

            // DialogueManager를 통해 게임 클리어 대화창을 띄움
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            if (dialogueManager != null)
            {
                dialogueManager.ShowGameClearDialogue();
            }

            // 게임 클리어 처리 (게임 종료)
            EndGame();
        }
    }

    private void EndGame()
    {
        // 게임 종료 처리
        Debug.Log("게임 클리어!");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
