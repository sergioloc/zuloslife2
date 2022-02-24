using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamilioController : MonoBehaviour {

    public float speed;
    public ParticleSystem fireParticles;

    private bool run;

    void Start() {
       
    }

    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.right * (speed/10) * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            run = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            StartCoroutine(StopRunning());
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            run = true;
            fireParticles.Stop();
        }
    }
    
    IEnumerator StopRunning() {
        yield return new WaitForSeconds(1f);
        run = false;
        fireParticles.Play();
    }

}
