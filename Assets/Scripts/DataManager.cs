using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    
    public int hiScore;
    public string hiScoreName;
    public string currentName;
    
    
    [Serializable]
    class SaveData
    {
        public string hiScoreName;
        public int hiScore;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    public void SaveScore()
    {
        SaveData data = new SaveData {hiScoreName = hiScoreName, hiScore = hiScore};

        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(Application.persistentDataPath+"/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            hiScoreName = data.hiScoreName;
            hiScore = data.hiScore;
        }
    }
    
}
