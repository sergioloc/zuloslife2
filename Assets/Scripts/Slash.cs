using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour {

    public float speed;
    public float lifetime;
    
    void Start() {
        Invoke("DestroySlash", lifetime);
    }

    void FixedUpdate() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            DestroySlash();
        }
    }

    private void DestroySlash() {
        Destroy(gameObject);
    }

}
