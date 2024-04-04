using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class tester : MonoBehaviour
{
    public string scoreOwnerName;
    public int score;

    HighscoreData data;
    // HighscoreData loadedData;
    string json;
    
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(GetDataPath()))
        {
            LoadHighscores();
        } else data = new HighscoreData();
    }

    public void SaveHighscores(){
        json = JsonUtility.ToJson(data);

        if(!File.Exists(GetDataPath())) {
            FileStream stream = new FileStream(GetDataPath(),FileMode.Create);
            stream.Close();
            Debug.Log("Created now save file");
        } else {Debug.Log("Save file found.");}

        File.WriteAllText(GetDataPath(), json);

        Debug.Log("Saved the following data " + json);
    }

    public void LoadHighscores(){
        data = JsonUtility.FromJson<HighscoreData>(File.ReadAllText(GetDataPath()));

        foreach (Highscore entry in data.scores)
        {
            Debug.Log(entry.score + " " + entry.name + " " + entry.dateTime);
        }
    }

    private string GetDataPath() {
        //testing
        return Path.Combine(Application.dataPath, "highscore.txt");
        //actual
        // return Path.Combine(Application.persistentDataPath, "highscore.txt");
    }

    public void SendNewHighScore() {
        
        if (data.IsNewHighscore(score))
        {
            data.UpdateHighscores(scoreOwnerName,score);
        } else { print("Not a highscore");}
    }

    public void GetHighscores(){
        List<Highscore> highscores = data.scores;
        
        foreach (var score in highscores)
        {
            print("By " + score.name + ", received " + score.score + " points on " + score.dateTime);
        }
    }


}
