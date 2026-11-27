using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private SaveData saveData;

    public string Name;

    public int Score;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    [System.Serializable]
    class SaveData
    {
        public string Name;
        public ScoreData HighScoreData;

        public List<ScoreData> PreviousScores;
    }

    [System.Serializable]
    class ScoreData
    {
        public string Name;

        public int Score;
    }

    private const string saveSuffix = "/savefile.json";

    public void LoadSave()
    {
        string path = Application.persistentDataPath + saveSuffix;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            saveData = data;
        }
    }

    public void SaveGame()
    {
        // Take the name and score and append it to the list of scores.

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + saveSuffix, json);
    }

    public void SaveName(string name)
    {
        // Only save if there is a name
        if (!string.IsNullOrEmpty(name))
        {
            saveData.Name = name;
            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(Application.persistentDataPath + saveSuffix, json);
        }
    }

    public string GetName()
    {
        return !string.IsNullOrEmpty(Name) ? Name : string.Empty;
    }

    public Tuple<string, int> GetHighScore()
    {
        // Get the highest score
        var highScore = saveData.PreviousScores.OrderByDescending(x => x.Score).FirstOrDefault();
        return new Tuple<string, int>(highScore.Name, highScore.Score);
    }
}
