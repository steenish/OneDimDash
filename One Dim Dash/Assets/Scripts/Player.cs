using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public void kill() {
        SceneManager.LoadScene(2);
        int finalScore = Mathf.RoundToInt(GameObject.Find("GameMaster").GetComponent<ScoreCounter>().currentScore);
        PlayerPrefs.SetInt("LastScore", finalScore);
    }
}
