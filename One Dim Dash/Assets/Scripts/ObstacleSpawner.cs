using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour { // TODO: Make sure nothing spawns inside last spawned obstacle and that the change in speed requirement is not too large. NEEDS TESTING AND CALIBRATION.

    public RectTransform spawnPoint;
    public PlayerState playerState;
    public GameObject[] obstaclePrefabs;

    public float largestWidth;      // <- THESE NEED TO BE CALIBRATED
    public float spawnExtraSpace;   // <-
    public float requiredSpawnSpace;

    public float timeToLikelySpawn;

    private float lastSpawnedTime;
    private GameObject lastSpawnedObstacle;

	// Use this for initialization
	void Start () {
        spawnObstacle();
        requiredSpawnSpace = largestWidth + spawnExtraSpace;
	}
	
	// Update is called once per frame
	void Update () {
        if (readyToSpawn()) {
            spawnObstacle();
        }
	}

    private bool readyToSpawn() {
        float timeSinceLastSpawn = Time.time - lastSpawnedTime;
        float lastSpeedRequirement = lastSpawnedObstacle.GetComponent<Obstacle>().speedRequirement;
        float lastXScale = lastSpawnedObstacle.transform.localScale.x;
        float distanceFromLast = timeSinceLastSpawn * playerState.speed - lastXScale / 2;

        return distanceFromLast >= requiredSpawnSpace; // If space is sufficient, the next obstacle will not spawn inside the last.

        // Deprecated way of calculating spawn probability
        /*float spawnProbability = (1 / timeToLikelySpawn) * timeSinceLastSpawn; // Probability to spawn an obstacle this update
        return Random.Range(0f, 1f) < spawnProbability;*/
    }

    private void spawnObstacle() {
        float speedRequirement = randomSpeedRequirement();
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);

        GameObject newObstacle = Instantiate(obstaclePrefabs[obstacleIndex], spawnPoint);
        if (newObstacle.transform.childCount > 0) {
            foreach (Transform child in newObstacle.transform) {
                GameObject childGameObject = child.gameObject;
                childGameObject.GetComponent<Obstacle>().speedRequirement = speedRequirement;
                lastSpawnedObstacle = childGameObject;
            }
        } else {
            newObstacle.GetComponent<Obstacle>().speedRequirement = speedRequirement;
            lastSpawnedObstacle = newObstacle;
        }

        lastSpawnedTime = Time.time;

        // Deprecated version of spawning.
        /*GameObject newObstacle = Instantiate(obstaclePrefabs[0], spawnPoint);
        newObstacle.transform.localScale = new Vector3(Random.Range(minWidth, maxWidth), 1, 1);
        newObstacle.GetComponent<Obstacle>().speedRequirement = speedRequirement;*/
    }

    private float randomSpeedRequirement() {
        return Random.Range(playerState.minSpeed, playerState.maxSpeed);
    }
}
