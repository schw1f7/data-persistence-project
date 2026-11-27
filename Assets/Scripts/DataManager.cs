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

    [SerializeField]
    private SaveData saveData = new SaveData
    {
        PreviousScores = new List<ScoreData>()
    };

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
    public class SaveData
    {
        public string Name;
        public ScoreData HighScoreData;

        public List<ScoreData> PreviousScores;
    }

    [System.Serializable]
    public class ScoreData
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

    public string GetName()
    {
        return saveData.Name;
    }

    public void SetName(string name)
    {
        saveData.Name = name;
    }

    public void SaveScore(ScoreData score)
    {
        saveData.PreviousScores.Add(score);
        SaveGame();
    }

    private void SaveGame()
    {
        // Take the name and score and append it to the list of scores.

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + saveSuffix, json);
    }

    public ScoreData GetHighScore()
    {
        // Get the highest score
        var highScore = saveData.PreviousScores.OrderByDescending(x => x.Score).FirstOrDefault();
        return highScore;
    }

    public List<ScoreData> GetTop10Scores()
    {
        return saveData.PreviousScores.OrderByDescending(x => x.Score).Take(10).ToList();
    }
}
