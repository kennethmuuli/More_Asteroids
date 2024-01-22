using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverScreen; 
    public TMP_Text finalScoreText; 
    public Scoretracker scoretracker; 

    private void OnEnable()
    {
        BaseDestructibleObject.objectDestroyed += OnObjectDestroyed;
    }

    private void OnDisable()
    {
        BaseDestructibleObject.objectDestroyed -= OnObjectDestroyed;
    }

    private void OnObjectDestroyed(int scoreValue)
    {
        // Kontrollime, kas hävitatud objekt oli Player, vaadates skoori väärtust
        // Kui mängija skoori väärtus on 0 
        if (scoreValue == 0) // mängija skoori väärtus 0, mida saab seadistada Inspectoris
        {
            GameOver();
        }
    }

    public void GameOver()
    {
                gameOverScreen.SetActive(true);

        // Uuendab lõppskoori teksti Scoretracker skripti praeguse skoori põhjal
        finalScoreText.text = "Final Score: " + scoretracker.GetCurrentScore().ToString();
    }
}
