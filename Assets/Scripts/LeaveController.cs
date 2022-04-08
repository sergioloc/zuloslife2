using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveController : MonoBehaviour {

    public float speed;
    public float lifetime;

    void Start() {
        Invoke("DestroyLeave", lifetime);
    }

    void FixedUpdate() {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Destroy(gameObject);
        }
    }

    private void DestroyLeave() {
        Destroy(gameObject);
    }

}
