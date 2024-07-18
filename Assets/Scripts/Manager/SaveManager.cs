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

    public void Reset()
    {
        coin = 0;
        playerHP = 100;
        playerHP_max = 100;
        playerlightgage = 100;
        playerlightgage_max = 100;
        savedSceneIndex = 0;
        inventorySlots.Clear();
    }
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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        path = Application.persistentDataPath + "/save";
        Debug.Log(path);
    }

    public void SaveData()
    {
        // 현재 인벤토리 데이터를 가져옵니다.
        nowPlayer.inventorySlots = InventoryMain.Instance.GetInventoryData();

        string filename = $"saveSlot_{nowSlot}.json";
        string filePath = Path.Combine(path, filename);

        string data = JsonUtility.ToJson(nowPlayer, true);

        // 경로를 출력하여 확인합니다 (디버깅용)
        Debug.Log("저장 파일 경로: " + filePath);
        File.WriteAllText(filePath, data);
    }

    public void LoadData()
    {
        nowPlayer.Reset(); // 데이터 초기화

        string filePath = Path.Combine(path, $"saveSlot_{nowSlot}.json");
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            nowPlayer = JsonUtility.FromJson<GameData>(data);

            // 불러온 인벤토리 데이터를 설정합니다.
            InventoryMain.Instance.SetInventoryData(nowPlayer.inventorySlots);
        }
        else
        {
            Debug.LogError("저장된 파일을 찾을 수 없습니다: " + filePath);
        }
    }

    public void DeleteData()
    {
        string filePath = Path.Combine(path, $"saveSlot_{nowSlot}.json");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("데이터 삭제 완료: " + filePath);
        }
        else
        {
            Debug.Log("삭제할 데이터가 없습니다: " + filePath);
        }

        DataClear();
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new GameData();
    }
}