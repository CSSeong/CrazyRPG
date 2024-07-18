using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessingManager : MonoBehaviour
{
    public static BlessingManager instance;

    [SerializeField]
    private CurseSelectionUI curseSelection;
    public CurseSelectionUI CurseSelection
    {
        get
        {
            return curseSelection;
        }
    }
    [SerializeField]
    private BlessingSelectionUI blessingSelection;
    public BlessingSelectionUI BlessingSelection
    {
        get
        {
            return blessingSelection;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Scene 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
