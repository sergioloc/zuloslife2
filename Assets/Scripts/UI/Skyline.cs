using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skyline : MonoBehaviour {

    public int speed;
    
    void Start() {
        transform.position = new Vector2(MenuValues.position, 0f);
    }

    void FixedUpdate() {
        if (MenuValues.moveRight) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x > 7)
                MenuValues.moveRight = false;
        }
        else {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x < -7)
                MenuValues.moveRight = true;
        }
        MenuValues.position = transform.position.x;
    }

}
