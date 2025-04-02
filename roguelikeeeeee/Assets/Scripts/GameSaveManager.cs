using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    public static string saveFilePath => Path.Combine(Application.persistentDataPath, "gameSave.json");
    private static GameSaveManager instance;

    private void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(this); }
        else Destroy(gameObject);
    }

    public static void SaveGame()
    {
        GameSaveData saveData = new GameSaveData
        {
            sceneName = "Game",
            playerPos = PlayerController.player.transform.position,
            health = PlayerController.player.health,
        };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);

        //Debug.Log("Game saved: " + saveFilePath);
        //Debug.Log("What saved: " + json);
    }

    public static GameSaveData LoadGame()
    {
        if (!isSaveExist())
        {
            //Debug.LogWarning("Save file not found!");
            return null;
        }

        string json = File.ReadAllText(saveFilePath);
        //Debug.Log("What loaded: " + json);
        return JsonUtility.FromJson<GameSaveData>(json);
    }

    public static bool isSaveExist() => File.Exists(saveFilePath);
}


[Serializable]
public class GameSaveData
{
    public string sceneName;
    public Vector2 playerPos; // Позиция игрока
    public float health; // Пример сохранения здоровья
}

