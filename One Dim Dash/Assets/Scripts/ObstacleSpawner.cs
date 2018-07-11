using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    public RectTransform spawnPoint;
    public PlayerState playerState;
    public GameObject[] obstaclePrefabs;
    public GameObject speedParticlePrefab;

    public float largestWidth;
    public float spawnExtraSpace;   // <-THIS MAY NEED TO BE CALIBRATED
    private float requiredSpawnSpace;
    
    public int maximumSpeedParticles;
    private float cameraSize;

    public float maximumChangeInSpeedRequirement;

    public float timeToLikelySpawn;

    private float lastSpawnedTime;
    private GameObject lastSpawnedObstacle;

	// Use this for initialization
	void Start () {
        spawnObstacle();
        requiredSpawnSpace = largestWidth + spawnExtraSpace;
        cameraSize = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize;

        spawnInitialSpeedParticles();
	}
	
	// Update is called once per frame
	void Update () {
        if (readyToSpawn()) {
            spawnObstacle();
        }

        if (readyToSpawnParticle()) {
            spawnParticle();
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
        float speedRequirement = (lastSpawnedObstacle == null) ? randomSpeedRequirement() : appropriateSpeedRequirement();
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

    // Returns a speed requirement that is not unrealistically far from the previous obstacle's requirement.
    private float appropriateSpeedRequirement() {
        float lastSpeedRequirement = lastSpawnedObstacle.GetComponent<Obstacle>().speedRequirement;
        float clampedRequirement = Mathf.Clamp(randomSpeedRequirement(), lastSpeedRequirement - maximumChangeInSpeedRequirement, lastSpeedRequirement + maximumChangeInSpeedRequirement);
        return Mathf.Clamp(clampedRequirement, playerState.minSpeed, playerState.maxSpeed);
    }

    private void spawnInitialSpeedParticles() {
        float leftLimit = GameObject.Find("DespawnCollider").transform.position.x + 5;
        float rightLimit = GameObject.Find("ObstacleSpawnPoint").transform.position.x;

        for (int i = 0; i < maximumSpeedParticles; i++) {
            float xPosition = Random.Range(leftLimit, rightLimit);
            float yPosition = randomYPosition();

            GameObject particle = Instantiate(speedParticlePrefab, spawnPoint);
            particle.transform.position = new Vector3(xPosition, yPosition, 0);
        }

        gameObject.GetComponent<ObstacleMover>().populateInitialSpeedParticles();
    }

    private float randomYPosition() {
        return Random.Range(-cameraSize, cameraSize);
    }

    private bool readyToSpawnParticle() {
        return gameObject.GetComponent<ObstacleMover>().speedParticles.Length <= maximumSpeedParticles;
    }

    private void spawnParticle() {
        GameObject particle = Instantiate(speedParticlePrefab, spawnPoint);
        particle.transform.position = new Vector3(particle.transform.position.x, randomYPosition(), particle.transform.position.z);
    }
}
