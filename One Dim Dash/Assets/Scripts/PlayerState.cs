using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    public float minSpeed, maxSpeed;
    public SpriteRenderer playerSprite;

    private float _speed;

    public float speed {
        get {
            return _speed;
        }

        set {
            _speed = Mathf.Clamp(value, minSpeed, maxSpeed);
            updateColor();
        }
    }

    void Start() {
        _speed = (maxSpeed - minSpeed) / 2 + 1;
    }

    private void updateColor() {
        // Calculate a value between 0 and 1 depending on _speed
        float colorValue = 1 - Mathf.Abs((11 - 2 * _speed)/9);

        // Change the color to red if speed is greater than 5.5, otherwise towards green
        if (_speed > 5.5) {
            playerSprite.color = new Color(1, colorValue, colorValue);
        } else {
            playerSprite.color = new Color(colorValue, 1, colorValue);
        }
    }
}
