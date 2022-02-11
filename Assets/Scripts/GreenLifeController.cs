using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLifeController : MonoBehaviour {

    public float speed;
    public GameObject projectile;
    public Transform shotPoint;

    private Rigidbody2D rb2d;
    private bool run;

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
        if (collision.gameObject.tag == "Ground") {
            run = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            run = false;
            Instantiate(projectile, shotPoint.position, transform.rotation);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            //run = true;
        }
    }

}
