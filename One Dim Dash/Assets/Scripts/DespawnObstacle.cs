using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnObstacle : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Obstacle") {
            Destroy(collision.gameObject);
        }
    }
}
