using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour, IActivate {

    public TextMeshProUGUI scoreText, turnCountText;
    public GameObject newHighScore;

    public void init(int score, int turnCount) {

        GameManager.SetScore(score);

        if (GameManager.HighScoreCheck()) {
            newHighScore.SetActive(true);
        } else {
            newHighScore.SetActive(false);
        }

        scoreText.text = score.ToString();
        turnCountText.text = turnCount.ToString();
    }


    public void Activate() {
        gameObject.SetActive(true);
    }

    public void DeActivate() {
        gameObject.SetActive(false);
    }

    public bool isActive() {
        return gameObject.activeInHierarchy;
    }
}
