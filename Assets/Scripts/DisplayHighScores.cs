using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreAsteroids.Score;

public class DisplayHighScores : MonoBehaviour
{
    [SerializeField] private GameObject soloScoresGrid, coopScoresGrid;
    [SerializeField] private TextMeshProUGUI scoreLablePrefab;
    private List<Highscore> soloHighscores, coopHighscores;
    bool dataDisplaying = false;

    private void OnEnable() {
        if (soloHighscores == null)
        {
            soloHighscores = SaveLoadSystem.instance.GetHighscores();
            coopHighscores = SaveLoadSystem.instance.GetHighscores(true);
        }

        if (!dataDisplaying)
        {
            ShowScores();
            dataDisplaying = true;
        }
    }

    private void ShowScores() {
        //soloScores
        foreach (Highscore soloScore in soloHighscores)
        {
            var listItem = Instantiate(scoreLablePrefab,soloScoresGrid.transform);
            listItem.text = soloScore.name + " : " + soloScore.score + " (" + soloScore.dateTime + ")";
        }

        //coopScores
        foreach (Highscore coopScore in coopHighscores)
        {
            var listItem = Instantiate(scoreLablePrefab,coopScoresGrid.transform);
            listItem.text = coopScore.name + " : " + coopScore.score + " (" + coopScore.dateTime + ")";
        }
    }
}
