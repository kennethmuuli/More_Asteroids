using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreAsteroids.Score {
    public class HighscoreData
    {
        public List<Highscore> scores = new List<Highscore>();

        public bool IsNewHighscore(int scoreToCheck) {

            Debug.Log(scores.Count);
            //first 10 scores
            if (scores.Count < 3)
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
            
            // Find the index where the new score should be inserted
            int insertIndex = 0;

            foreach (Highscore score in scores) {
                if (scoreToAdd > score.score) {
                    break;
                }
                insertIndex++;
            }

            // Shift down lower scores
            for (int i = scores.Count - 1; i > insertIndex; i--) {
                scores[i] = scores[i - 1];
            }

            // Insert the new high score
            scores[insertIndex] = newHighScore;

            // Remove the last score if the list exceeds 3 scores
            if (scores.Count > 3) {
                scores.RemoveAt(scores.Count - 1);
            }
        }
    }

    [System.Serializable]
    public class Highscore {

        public string name;
        public int score;
        public string dateTime;

        public Highscore(string newScoreName, int newScore) {
            this.name = newScoreName;
            this.score = newScore;
            this.dateTime = DateTime.Now.ToString();
        }
    }

}
