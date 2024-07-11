using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameData
{
    public int coin = 0;
    public float playerHP = 100;
    public float playerHP_max = 100;
    public float playerlightgage = 100;
    public float playerlightgage_max = 100;
    public int savedSceneIndex = 0;

    public List<InventorySlotData> inventorySlots = new List<InventorySlotData>();
}

[System.Serializable]
public class InventorySlotData
{
    public int itemID;
    public int itemCount;
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
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        #endregion

        path = Application.persistentDataPath + "/save";
        print(path);
    }

    public void SaveData()
    {
        nowPlayer.inventorySlots.Clear();
        // �κ��丮 �����͸� ������
        nowPlayer.inventorySlots = InventoryMain.Instance.GetInventoryData();

        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData()
    {
        if (ItemDatabase.Instance == null)
        {
            Debug.LogError("ItemDatabase.Instance is null. Make sure the ItemDatabase script is attached to a GameObject in the scene.");
            return;
        }

        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<GameData>(data);

        // �κ��丮 ������ ����
        InventoryMain.Instance.SetInventoryData(nowPlayer.inventorySlots);
    }

    public void DeleteData()
    {
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

        DataClear();
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new GameData();
    }
}
