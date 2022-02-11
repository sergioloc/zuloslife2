using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLifeController : MonoBehaviour {

    private Rigidbody2D rb2d;
    private bool run;

    [SerializeField]
    private float speed;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        run = false;
    }

    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Floor") {
            run = true;
        }
    }
}
