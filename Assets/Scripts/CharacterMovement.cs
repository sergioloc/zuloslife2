using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    [Header("Values")]
    public float speed;
    public float limit = 2.5f;

    [Space()]
    public List<string> targetsTag;
    private string targetTag;

    private List<Target> enemies = new List<Target>();
    private Target target;

    private Animator animator;
    private bool run;
    private bool lookRight = true;
    private bool targetActive = false;
    private float distanceToTarget;

    void Start() {
        targetTag = targetsTag[0];
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        // Run forward if there are no enemies in area
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
            
            Target t = new Target(collision.transform, collision.gameObject.tag);

            // Add enemy to the list if is not already in
            if (!ListContains(t)) {
                enemies.Add(t);
                //Debug.Log("Enemies in area: " + enemies.Count);
            }

            // Check if next target is available
            if (enemies.Count > 0) {
                target = enemies[SearchTarget()];
                targetActive = true;
                animator.SetBool("isAttacking", true);
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
                //Debug.Log("Enemies in area: " + enemies.Count);
            }

            // Check if all enemies are gone
            if (enemies.Count == 0) {
                targetActive = false;
                animator.SetBool("isAttacking", false);
                run = true;
                if (!lookRight) {
                    LookRight();
                }
            }
            else
                target = enemies[SearchTarget()];
        }
    }

    // Check which direction must character look
    private void LookAtTarget() {
        if (distanceToTarget > 0 && !lookRight) {
            LookRight();
        }
        else if (distanceToTarget < 0 && lookRight) {
            LookLeft();
        }
    }

    // Face character to the right
    private void LookRight() {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        lookRight = true;
        limit = Mathf.Abs(limit);
    }

    // Face character to the left
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
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i].tag == targetTag) {
                return i;
            }
        }

        // If there are no enemies in area, get nearest enemy
        return NearestEnemy();
    }

    // Get nearest enemy from area
    private int NearestEnemy() {
        float[] distances = new float[enemies.Count];

        for (int i = 0; i < enemies.Count; i++) {
            distances[i] = (Mathf.Abs(enemies[i].transform.position.x - transform.position.x));
        }

        float minDistance = Mathf.Min(distances);
        int index = -1;

        for (int i = 0; i < distances.Length; i++) {
            if (minDistance == distances[i])
                index = i;
        }
        return index;
    }

    // Check if target is already in enemies list
    private bool ListContains(Target t) {
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i].transform.position.x == t.transform.position.x && enemies[i].tag == t.tag)
                return true;
        }
        return false;
    }

    // Remove target from enemies list
    private void RemoveFromList(Target t) {
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i].transform.position.x == t.transform.position.x && enemies[i].tag == t.tag) {
                enemies.Remove(enemies[i]);
                break;
            }
        }
    }

    // Check if tag belongs to target enemy or common enemy
    private bool isEnemy(string tag) {
        if (targetTag == tag)
            return true;
        else {
            for (int i = 0; i < targetsTag.Count; i++) {
                if (targetsTag[i] == tag)
                    return true;
            }
        }
        return false;
    }

}
