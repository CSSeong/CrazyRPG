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
        #region �̱���
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
        // ���� ������ ������ ������ �����ϰ�, ���� �����͸� �ʱ�ȭ�մϴ�.
        string filePath = path + nowSlot.ToString();

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("������ ���� �Ϸ�: " + filePath);
        }
        else
        {
            Debug.Log("������ �����Ͱ� �����ϴ�: " + filePath);
        }

        DataClear(); // ������ �ʱ�ȭ �޼��� ȣ��
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new GameData();
    }
}
