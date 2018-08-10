using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public PlayerState playerState;

    public float accelerationMultiplier;
    public float touchAccelerationMultiplier;

    private Rect leftRect;
    // private Rect rightRect; // really only a leftRect is needed

    void Start() {
        leftRect = new Rect(0, 0, Screen.width / 2, Screen.height);
        //rightRect = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height);
    }

    // Update is called once per frame
    void Update () {
        if (Input.touchCount > 0) {
            int touchDirection = (leftRect.Contains(Input.GetTouch(0).position)) ? -1 : 1; // Determines whether the left or right side of the screen is being touched, and sets the direction of acceleration
            updateSpeed(touchDirection);
        } else if (Input.GetMouseButton(0)) {
            int touchDirection = (leftRect.Contains(Input.mousePosition)) ? -1 : 1;
            updateSpeed(touchDirection);
        }

        // Deprecated
        //float horizontalInput = Input.GetAxis("Horizontal");
        //updateSpeed(horizontalInput);
    }

    // Deprecated
    private void updateSpeed(float horizontalInput) {
        playerState.speed = playerState.speed + horizontalInput * Time.deltaTime * accelerationMultiplier;
    }

    private void updateSpeed(int touchDirection) {
        playerState.speed = playerState.speed + touchDirection * Time.deltaTime * touchAccelerationMultiplier;
    }
}
