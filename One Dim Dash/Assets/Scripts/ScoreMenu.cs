using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreMenu : MonoBehaviour {

    public void restart() {
        SceneManager.LoadScene(1);
    }

    public void returnToMenu() {
        SceneManager.LoadScene(0);
    }

    public void playSelectSound() {
//FIX HERE
    }
}
