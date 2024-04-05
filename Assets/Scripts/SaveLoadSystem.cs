using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MoreAsteroids.Score;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem instance;
    private HighscoreData soloData, coopData;
    private string jsonData;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        } else {
            Destroy(gameObject);
        } 

        DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        //Load in or setup solo scores
        if (File.Exists(GetDataPath()))
        {
            LoadHighscores();
        } else {
            soloData = new HighscoreData();
            PopulateEmptyScores(soloData);
            SaveHighscores();
        }

        //Load in or setup coop scores
        if (File.Exists(GetDataPath(true)))
        {
            LoadHighscores();
        } else {
            coopData = new HighscoreData();
            PopulateEmptyScores(coopData);
            SaveHighscores(true);
        }


    }

    private void PopulateEmptyScores(HighscoreData data){
        Highscore emptyScore = new Highscore("EMPTY", 0)
        {
            dateTime = "--/--/--"
        };

        for (int i = 0; i < 3; i++)
        {
            data.scores.Add(emptyScore);
        }
    }

    private void LoadHighscores(){
        soloData = JsonUtility.FromJson<HighscoreData>(File.ReadAllText(GetDataPath()));
        coopData = JsonUtility.FromJson<HighscoreData>(File.ReadAllText(GetDataPath(true)));
    }

    private string GetDataPath(bool getCoop = false) {
        //testing
        // if (getCoop)
        // {
        //     return Path.Combine(Application.dataPath, "coop_highscore.txt");
        // }
        // return Path.Combine(Application.dataPath, "solo_highscore.txt");
        //actual
        if (getCoop)
        {
            return Path.Combine(Application.persistentDataPath, "coop_highscore.txt");
        }
        return Path.Combine(Application.persistentDataPath, "highscore.txt");
    }

    public void SaveHighscores(bool saveCoop = false){
        if (saveCoop)
        {
            jsonData = JsonUtility.ToJson(coopData);
        } else jsonData = JsonUtility.ToJson(soloData);

        if(!File.Exists(GetDataPath(saveCoop))) {
            FileStream stream = new FileStream(GetDataPath(saveCoop),FileMode.Create);
            stream.Close();
            Debug.Log("No existing save file found. Created now save file at: " + GetDataPath(saveCoop));
        } else {Debug.Log("Save file found.");}

        File.WriteAllText(GetDataPath(saveCoop), jsonData);

        Debug.Log("Saved the following data " + jsonData);
    }

    public bool CheckIfHighScore (int scoreToCheck, bool forCoop = false) => forCoop == false ? soloData.IsNewHighscore(scoreToCheck) : coopData.IsNewHighscore(scoreToCheck);
    public void InsertHighScore(string scoreOwnerName, int scoreToAdd, bool forCoop = false) 
    {
        if (forCoop)
        {
            coopData.UpdateHighscores(scoreOwnerName, scoreToAdd);
        } 
        else 
        {
            soloData.UpdateHighscores(scoreOwnerName, scoreToAdd);
        }

        SaveHighscores(forCoop);
    }

    public List<Highscore> GetHighscores(bool forCoop = false) => forCoop == false ? soloData.scores : coopData.scores;
}
