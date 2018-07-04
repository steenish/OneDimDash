using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    private float _speedRequirement = 1; // Default speed requirement

    public float speedRequirement {
        get {
            return _speedRequirement;
        }
        set {
            _speedRequirement = value;
            setColor();
        }
    }

    public int speedThreshold;

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            GameObject player = collision.gameObject;
            float playerSpeed = player.GetComponent<PlayerState>().speed;
            
            if (!withinSpeedThreshold(playerSpeed)) {
                player.GetComponent<Player>().kill();
            }
        }
    }

    private bool withinSpeedThreshold(float speed) {
        return Mathf.Abs(speedRequirement - speed) < speedThreshold;
    }

    private void setColor() {
        // Calculate a value between 0 and 1 depending on _speed
        float colorValue = 1 - Mathf.Abs((11 - 2 * _speedRequirement) / 9);

        // Change the color to red if speed is greater than 5.5, otherwise towards green
        if (_speedRequirement > 5.5) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, colorValue, colorValue);
        } else {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(colorValue, 1, colorValue);
        }
    }
}
