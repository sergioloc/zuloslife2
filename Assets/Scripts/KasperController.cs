using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasperController : MonoBehaviour {

    public float health = 100;
    public float speed;
    public ParticleSystem inhaleParticles;
    public GameObject deathParticles;

    private bool run;
    private bool isBurning;
    private Animator animator;

    void Start() {
        run = true;
        isBurning = false;
        animator = GetComponent<Animator>();
    }
    
    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.left * (speed/10) * Time.deltaTime);
        }
        if (isBurning) {
            health = health - 1;
            if (health <= 0) {
                Instantiate(deathParticles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Fire") {
            isBurning = true;
            animator.SetBool("isBurning", true);
        }
        else if (collision.gameObject.tag == "Shield") {
            run = false;
            animator.SetTrigger("Attack");
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Fire") {
            isBurning = false;
            animator.SetBool("isBurning", false);
        }
    }

    void Inhale() {
        inhaleParticles.Play();
    }
    
}
