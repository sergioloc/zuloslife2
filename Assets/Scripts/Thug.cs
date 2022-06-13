using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thug : MonoBehaviour {
    
    public float speed;
    public float lifetime;

    void Start() {
        Invoke("DestroyThug", lifetime);
    }

    void FixedUpdate() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void DestroyThug() {
        Destroy(gameObject);
    }

}
