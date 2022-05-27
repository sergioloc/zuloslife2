using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rose : MonoBehaviour {
    
    [Header("Values")]
    [SerializeField] private float health = 100;
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    [Header("Thunder")]
    [SerializeField] private ParticleSystem thunderParticles;
    [SerializeField] private AudioSource thunderSound;

    private bool run;
    private Animator animator;

    void Start() {
        run = false;
        animator = GetComponent<Animator>();
    }

   
    void Update() {
        if (run) {
            transform.Translate(Vector2.left * (speed/10) * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (!collision.gameObject.GetComponent<PlayerMovement>().reverse) {
                run = false;
                animator.SetTrigger("Attack");
            }
            else {
                run = true;
            }
        }
    }

    void Run() {
        run = true;
    }

    void Thunder() {
        thunderParticles.Play();
        thunderSound.Play();
    }

}
