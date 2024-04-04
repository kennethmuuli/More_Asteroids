using System;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreData
{
    public List<Highscore> scores = new List<Highscore>();

    // public HighscoreData() {
    //     populateEmptyScores();
    // }
    // private void populateEmptyScores(){
    //     Highscore emptyScore = new Highscore("EMPTY", 0);
    //     emptyScore.dateTime = DateTime.MinValue;
        
    //     for (int i = 0; i < 10; i++)
    //     {
    //         scores.Add(emptyScore);
    //     }
    // }

    public bool IsNewHighscore(int scoreToCheck) {

        Debug.Log(scores.Count);
        //first 10 scores
        if (scores.Count < 10)
        {
            return true;
        }

        for (int i = 0; i < scores.Count; i++)
        {
            if (scoreToCheck > scores[i].score) {
                return true;
            }
        }

        return false;
    }

    public void UpdateHighscores(string scoreOwnerName, int scoreToAdd) {
        
        Highscore newHighScore = new Highscore(scoreOwnerName, scoreToAdd);

        if (scores.Count == 0)
        {
            scores.Add(newHighScore);
            return;
        } else if (scores.Count < 10)
        {
            for (int i = scores.Count-1; i >= 0 ; i--)
            {
                if (scoreToAdd > scores[i].score) {
                    scores.Insert(i+1,newHighScore);
                    return;
                }
            }
        } else {
            for (int i = scores.Count-1; i >= 0 ; i--)
            {
                if (scoreToAdd > scores[i].score) {
                    scores[i] = newHighScore;
                    return;
                }
            }
        }

    }
}


[System.Serializable]
public class Highscore {

    public string name;
    public int score;
    public DateTime dateTime;

    public Highscore(string newScoreName, int newScore) {
        this.name = newScoreName;
        this.score = newScore;
        this.dateTime = DateTime.Now;
    }
}
