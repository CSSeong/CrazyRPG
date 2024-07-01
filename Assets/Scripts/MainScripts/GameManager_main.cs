using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_main : MonoBehaviour
{
    private void Awake()
    {
        SaveManager.instance.LoadData();
    }
}
