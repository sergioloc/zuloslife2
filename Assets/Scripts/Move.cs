using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    
    [SerializeField] private float speed;
    [SerializeField] private bool moveLeft;

    private Vector2 direction;

    void Start() {
        if (moveLeft)
            direction = Vector2.left;
        else
            direction = Vector2.right;
    }

    void FixedUpdate() {
        transform.Translate(direction * speed * Time.deltaTime);
    }

}
