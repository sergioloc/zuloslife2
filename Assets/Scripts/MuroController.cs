using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroController : MonoBehaviour {

    public float speed;

    private bool run;
    private Animator animator;

    void Start() {
        run = true;
        animator = GetComponent<Animator>();
    }

    
    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.left * (speed/10) * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            run = false;
            animator.SetBool("isWall", true);
        }
    }
    
}
