using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour {

    public PlayerState playerState;
    public float obstacleSpeedMultiplier;

    private float playerSpeed;

    private GameObject[] obstacles;
	
	// Update is called once per frame
	void Update () {
        playerSpeed = playerState.speed;
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles) {
            obstacle.transform.position = new Vector3(obstacle.transform.position.x - playerSpeed * Time.deltaTime * obstacleSpeedMultiplier, 0, 0);
        }
	}
}
