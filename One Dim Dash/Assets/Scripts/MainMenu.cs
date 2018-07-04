using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public void startGame() {
        SceneManager.LoadScene(1);
    }

    public void quit() {
        Application.Quit();
    }

    public void loadHighScore() {
        Text highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        highScoreText.text = formatScoreString(PlayerPrefs.GetInt("HighScore", 0));
    }

    public void resetHighScore() {
        Text highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        PlayerPrefs.SetInt("HighScore", 0);
        highScoreText.text = formatScoreString(PlayerPrefs.GetInt("HighScore", 0));
    }

    private string formatScoreString(int score) {
        string scoreString = "";

        if (score < 100) {
            scoreString += "0";
            if (score < 10) {
                scoreString += "0";
            }
        }
        return "High Score: " + scoreString + score.ToString();
    }
}
