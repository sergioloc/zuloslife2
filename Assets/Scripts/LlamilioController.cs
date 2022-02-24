using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamilioController : MonoBehaviour {

    public float speed;
    public ParticleSystem fireParticles;
    public GameObject target;

    private bool run;
    private float distanceToTarget;
    private bool lookRight = true;
    private float limit = 2.3f;

    void Start() {
       
    }

    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.right * (speed/10) * Time.deltaTime);
        }
        else if (!lookRight) {
            FollowTarget();
        }
        if (target != null) {
            distanceToTarget = target.transform.position.x - transform.position.x;
            LookAtTarget();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground" && !run) {
            run = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Kasper" && run) {
            fireParticles.Play();
            run = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Kasper") {
            fireParticles.Stop();
            run = true;
            target = null;
            if (!lookRight) {
                LookRight();
            }
        }
    }

    private void LookAtTarget() {
        if (distanceToTarget > 0 && !lookRight) {
            LookRight();
        }
        else if (distanceToTarget < 0 && lookRight) {
            LookLeft();
        }
    }

    private void LookRight() {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        lookRight = true;
        limit = Mathf.Abs(limit);
    }

    private void LookLeft() {
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        lookRight = false;
        limit = -Mathf.Abs(limit);
    }

    private void FollowTarget() {
        if (Mathf.Abs(distanceToTarget) >= 2.3) {
            Vector3 position = new Vector3(target.transform.position.x - limit, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }
    }

}
