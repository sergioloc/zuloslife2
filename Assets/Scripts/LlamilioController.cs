using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamilioController : MonoBehaviour {

    [Header("Values")]
    public float speed;
    public float limit = 2.5f;

    [Header("Particles")]
    public ParticleSystem fireParticles;

    [Header("Targets in area")]
    public List<Transform> targets;

    private Transform target;
    private bool run;
    private float distanceToTarget;
    private bool lookRight = true;

    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.right * (speed/10) * Time.deltaTime);
        }
        else if (!lookRight) {
            FollowTarget();
        }

        if (targets.Count == 0) {
            target = null;
        }
        else
            target = targets[NearestTarget()];

        if (target != null) {
            distanceToTarget = target.transform.position.x - transform.position.x;
            LookAtTarget();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground" && !run && target == null) {
            run = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Kasper") {
            if (!targets.Contains(collision.transform))
                targets.Add(collision.transform);
            run = false;
            fireParticles.Play();
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Kasper") {
            if (targets.Contains(collision.transform))
                targets.Remove(collision.transform);

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

    // Follow nearest target from area
    private void FollowTarget() {
        if (Mathf.Abs(distanceToTarget) >= 2.3) {
            Vector3 position = new Vector3(target.transform.position.x - limit, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }
    }

    // Get nearest target from area
    private int NearestTarget() {
        float[] distances = new float[targets.Count];

        for (int i = 0; i < targets.Count; i++)
        {
            distances[i] = (Mathf.Abs(targets[i].position.x - transform.position.x));
        }

        float minDistance = Mathf.Min(distances);
        int index = -1;

        for (int i = 0; i < distances.Length; i++)
        {
            if (minDistance == distances[i])
                index = i;
        }
        return index;
    }

}
