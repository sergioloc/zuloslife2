using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasperController : MonoBehaviour {

    public float speed;
    public ParticleSystem inhaleParticles;

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

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Shield") {
            run = false;
            animator.SetTrigger("Attack");
        }
    }

    void Inhale() {
        inhaleParticles.Play();
    }
    
}
