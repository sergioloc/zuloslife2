using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasperController : MonoBehaviour {

    public float speed;

    private bool run;
    private Animator animator;

    void Start() {
        run = true;
        animator = GetComponent<Animator>();
    }

    
    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Shield") {
            run = false;
            animator.SetTrigger("Attack");
        }
    }
    
}
