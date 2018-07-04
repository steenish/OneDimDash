using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour {

    public Text ScoreIntText;
    public Text HighScoreIntText;

	// Use this for initialization
	void Start () {
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (lastScore > highScore) {
            PlayerPrefs.SetInt("HighScore", lastScore);
            highScore = lastScore;
        }

        ScoreIntText.text = formatScoreString(lastScore);
        HighScoreIntText.text = formatScoreString(highScore);
    }

    private string formatScoreString(int score) {
        string scoreString = "";

        if (score < 100) {
            scoreString += "0";
            if (score < 10) {
                scoreString += "0";
            }
        }
        return scoreString + score.ToString();
    }
}
