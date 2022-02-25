using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamilioController : MonoBehaviour {

    [Header("Values")]
    public float speed;
    public float limit = 2.5f;

    [Header("Enemies")]
    public string targetTag;
    public List<string> enemiesTag;

    [Header("Particles")]
    public ParticleSystem fireParticles;
    public GameObject damage;

    [Header("Targets in area")]
    public List<Target> targets = new List<Target>();

    private Target target;
    private bool run;
    private bool lookRight = true;
    private bool targetActive = false;
    private float distanceToTarget;

    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.right * (speed/10) * Time.deltaTime);
        }
        // If enemy is behind you, follow him
        else if (!lookRight) {
            FollowTarget();
        }

        // If there is a target in area, look at him
        if (targetActive) {
            distanceToTarget = target.transform.position.x - transform.position.x;
            LookAtTarget();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // Start running whem character touches ground
        if (collision.gameObject.tag == "Ground" && !run && target == null) {
            run = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (isEnemy(collision.gameObject.tag)) {
            
            Target t = new Target(collision.transform, targetTag);

            // Add enemy to the list if is not already in
            if (!ListContains(t)) {
                targets.Add(t);
                Debug.Log("Enemies in area: " + targets.Count);
            }

            // Check if next target is available
            if (targets.Count > 0) {
                target = targets[SearchTarget()];
                targetActive = true;
                //fireParticles.Play();
                //damage.SetActive(true);
                run = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (isEnemy(collision.gameObject.tag)) {
            Target t = new Target(collision.transform, targetTag);

            // Remove enemy from list if exists
            if (ListContains(t)) {
                RemoveFromList(t);
                Debug.Log("Enemies in area: " + targets.Count);
            }

            // Check if all enemies are gone
            if (targets.Count == 0) {
                targetActive = false;
                //fireParticles.Stop();
                //damage.SetActive(false);
                run = true;
                if (!lookRight) {
                    LookRight();
                }
            }
            else
                target = targets[SearchTarget()];
            
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

    // Follow nearest enemy from area
    private void FollowTarget() {
        if (Mathf.Abs(distanceToTarget) >= Mathf.Abs(limit)) {
            Vector3 position = new Vector3(target.transform.position.x - limit, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }
    }

    // Get target enemy from area
    private int SearchTarget() {
        for (int i = 0; i < targets.Count; i++) {
            if (targets[i].tag == targetTag) {
                return i;
            }
        }

        // If there are no targets in area, get nearest enemy
        return NearestEnemy();
    }

    // Get nearest enemy from area
    private int NearestEnemy() {
        float[] distances = new float[targets.Count];

        for (int i = 0; i < targets.Count; i++) {
            distances[i] = (Mathf.Abs(targets[i].transform.position.x - transform.position.x));
        }

        float minDistance = Mathf.Min(distances);
        int index = -1;

        for (int i = 0; i < distances.Length; i++) {
            if (minDistance == distances[i])
                index = i;
        }
        return index;
    }

    private bool ListContains(Target t) {
        for (int i = 0; i < targets.Count; i++) {
            //Debug.Log("Is equals? --->" + targets[i].toString() + " | " + t.toString() + " ---- " + (targets[i].transform.position.x == t.transform.position.x) + ", " + (targets[i].tag == t.tag));
            if (targets[i].transform.position.x == t.transform.position.x && targets[i].tag == t.tag)
                return true;
        }
        return false;
    }

    private void RemoveFromList(Target t) {
        for (int i = 0; i < targets.Count; i++) {
            if (targets[i].transform.position.x == t.transform.position.x && targets[i].tag == t.tag) {
                targets.Remove(targets[i]);
                break;
            }
        }
    }

    private bool isEnemy(string tag) {
        if (targetTag == tag)
            return true;
        else {
            for (int i = 0; i < enemiesTag.Count; i++) {
                if (enemiesTag[i] == tag)
                    return true;
            }
        }
        return false;
    }

}
