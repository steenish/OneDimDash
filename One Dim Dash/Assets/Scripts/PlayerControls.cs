using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public PlayerState playerState;

    public float accelerationMultiplier;

	// Update is called once per frame
	void Update () {
        float horizontalInput = Input.GetAxis("Horizontal");

        updateSpeed(horizontalInput);
	}

    private void updateSpeed(float horizontalInput) {
        playerState.speed = playerState.speed + horizontalInput * Time.deltaTime * accelerationMultiplier;
    }
}
