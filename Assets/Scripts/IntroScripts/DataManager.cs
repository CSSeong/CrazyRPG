using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameData
{
    public int coin = 0;
    public float hp = 100;
    public float lightgage = 100;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public GameData nonPlayer = new GameData();

    public string path;
    public string filename = "save";
    public int nowSlot;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/";
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nonPlayer);
        File.WriteAllText(path, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + filename);
        nonPlayer = JsonUtility.FromJson<GameData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nonPlayer = new GameData();
    }
}
