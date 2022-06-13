using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kasper : MonoBehaviour {

    [Header("Values")]
    public float speed;
    public float fireDamage;

    [Header("Particles")]
    public ParticleSystem inhaleParticles;
    public GameObject deathParticles;
    public Transform deathPoint;

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
