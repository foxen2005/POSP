using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int coins;
    public bool faction; // true para gatos, false para vacas
    public int[] soldiers = new int[5];
    public int[] soldierLevels = new int[5];
    public int historicalProgress;
}

public class DataManager : MonoBehaviour
{
    private string dataPath;

    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "\\Assets\\DATA.json");
    }

    public void SaveData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(dataPath, json);
    }

    public PlayerData LoadData()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        return new PlayerData(); // Retorna datos vacíos si el archivo no existe
    }
}
