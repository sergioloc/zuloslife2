using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thug : MonoBehaviour {
    
    public float speed;

    void FixedUpdate() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

}
