using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLifeController : MonoBehaviour {

    public float speed;
    public float rotateSpeed;
    public GameObject projectile;
    public Transform head;

    [Header("Targets in area")]
    public List<Transform> targets;

    private Rigidbody2D rb2d;
    private bool run, shooting = false;
    private float currentAngle = 0;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        run = false;
    }

    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            run = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            shooting = true;
            StartCoroutine(StopRunning());
            StartCoroutine(Shoot());
            if (!targets.Contains(collision.transform))
                targets.Add(collision.transform);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            FindTarget();
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            run = true;
            shooting = false;
            if (targets.Contains(collision.transform)){
                targets.Remove(collision.transform);
            }
        }
    }

    IEnumerator StopRunning() {
        yield return new WaitForSeconds(1f);
        run = false;
    }

    private void FindTarget() {
        int index = NearestTarget();
        if (index != -1) 
            LookAt(targets[index].position);
    }

    private void LookAt(Vector2 target) {
        float distanceX = target.x - transform.position.x;
        float distanceY = target.y - transform.position.y;
        currentAngle = Mathf.Atan2(distanceX, distanceY) * Mathf.Rad2Deg;
        Quaternion endRotation = Quaternion.AngleAxis(currentAngle, Vector3.back);
        head.rotation = Quaternion.Slerp(head.rotation, endRotation, Time.deltaTime * rotateSpeed);
    }

    private int NearestTarget() {
        float[] distances = new float[targets.Count];

        for (int i = 0; i < targets.Count; i++) {
            distances[i] = (Mathf.Abs(targets[i].position.x - transform.position.x));
        }

        float minDistance = Mathf.Min(distances);
        int index = -1;

        for (int i = 0; i < distances.Length; i++) {
            if (minDistance == distances[i])
                index = i;
        }
        return index;
    }

    IEnumerator Shoot() {
        yield return new WaitForSeconds(1f);
        Quaternion finalRotation = Quaternion.AngleAxis(currentAngle - 45, Vector3.back);
        Instantiate(projectile, head.position, finalRotation);
        if (shooting)
             StartCoroutine(Shoot());
    }

}
