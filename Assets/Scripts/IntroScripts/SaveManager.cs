using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class GameData
{
    public int coin = 0;
    public int firewood = 0;
    public float playerHP = 100;
    public float playerlightgage = 100;
    public int savedSceneIndex = 0;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance; 

    public GameData nowPlayer = new GameData(); 

    public string path; 
    public int nowSlot;

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/save";
        print(path);
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<GameData>(data);
    }

    public void DeleteData()
    {
        // 현재 슬롯의 데이터 파일을 삭제하고, 관련 데이터를 초기화합니다.
        string filePath = path + nowSlot.ToString();

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("데이터 삭제 완료: " + filePath);
        }
        else
        {
            Debug.Log("삭제할 데이터가 없습니다: " + filePath);
        }

        DataClear(); // 데이터 초기화 메서드 호출
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new GameData();
    }
}
