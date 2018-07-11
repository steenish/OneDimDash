using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour {

    public PlayerState playerState;
    public float obstacleSpeedMultiplier;

    private float playerSpeed;

    private GameObject[] obstacles;
    public GameObject[] speedParticles { get; private set; }

    // Update is called once per frame
    void Update () {
        playerSpeed = playerState.speed;
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        speedParticles = GameObject.FindGameObjectsWithTag("SpeedParticle");

        foreach (GameObject obstacle in obstacles) {
            obstacle.transform.position = new Vector3(obstacle.transform.position.x - playerSpeed * Time.deltaTime * obstacleSpeedMultiplier, 0, 0);
        }

        foreach (GameObject speedParticle in speedParticles) {
            speedParticle.transform.position = new Vector3(speedParticle.transform.position.x - playerSpeed * Time.deltaTime * obstacleSpeedMultiplier, 
                speedParticle.transform.position.y, speedParticle.transform.position.z);
        }
	}

    public void populateInitialSpeedParticles() {
        speedParticles = GameObject.FindGameObjectsWithTag("SpeedParticle");
    }
}
