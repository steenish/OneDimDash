using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    public Text scoreText;
    public PlayerState playerState;

    public float scoreMultiplier;

    public float currentScore { get; private set; }

    private bool _isCounting; 

    public bool isCounting { get; private set; }

    void Start() {
        currentScore = 0;
        isCounting = true; // Starts counting at object load by default
    }

    public void startCounting() {
        isCounting = true;
    }

    public void stopCounting() {
        isCounting = false;
    }

	// Update is called once per frame
	void Update () {
        if (isCounting) {
            float dDistance = playerState.speed * Time.deltaTime;
            currentScore += dDistance * scoreMultiplier;

            updateScoreText();
        }
	}

    // Updates the UI score text with the current score
    private void updateScoreText() {
        StringBuilder scoreString = new StringBuilder();

        // Add zeros to the front if needed
        if (currentScore < 100) {
            scoreString.Append(0);
            if (currentScore < 10) {
                scoreString.Append(0);
            }
        }

        scoreString.Append(Mathf.RoundToInt(currentScore));

        scoreText.text = scoreString.ToString();
    }
}
