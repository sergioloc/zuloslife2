using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pb : MonoBehaviour {

    [Header("Values")]
    [SerializeField] private float health = 100;
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    [Header("Attack")]
    [SerializeField] private ParticleSystem absorbParticles;
    [SerializeField] private AudioSource absorbSound;

    private bool run;
    private Animator animator;
    
    void Start() {
        run = true;
        animator = GetComponent<Animator>();
    }
   
    void Update() {
        if (run) {
            transform.Translate(Vector2.left * (speed/10) * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Absorb");
            run = false;
            absorbParticles.Play();
            absorbSound.Play();
            animator.SetBool("isAttacking", true);
        }
        else if (collision.gameObject.tag == "Shield") {
            run = false;
            absorbParticles.Play();
            absorbSound.Play();
            animator.SetBool("isAttacking", true);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            StartCoroutine(StartRun());
            absorbParticles.Stop();
            animator.SetBool("isAttacking", false);
        }
    }

    private IEnumerator StartRun() {
        yield return new WaitForSeconds(1f);
        run = true;
    }

    public void PlayAbsorb() {
        absorbSound.Play();
    }

}
