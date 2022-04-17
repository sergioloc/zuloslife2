using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLifeController : MonoBehaviour {

    public float speed;
    public float rotateSpeed;
    public float recoil;
    public GameObject projectile;
    public Transform head;
    public Transform shotPoint;
    public ParticleSystem tornadoParticles;

    [Header("Shadow")]
    public GameObject shadow;
    public GameObject shadowPosition;
    public float height;
    private GameObject newShadow;

    [Header("Sounds")]
    public AudioSource fallingSound;
    public AudioSource landSound;
    public AudioSource tornadoSound;

    [Header("Targets in area")]
    public List<Transform> targets;

    private Rigidbody2D rb2d;
    private Animator animator;
    private bool run, shooting = false, hasLanded = false;
    private float currentAngle = 0;

    void Start() {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        run = false;
        newShadow = Instantiate(shadow, new Vector3(transform.position.x, height, transform.position.z), Quaternion.identity);
    }

    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            newShadow.transform.parent = gameObject.transform;
            newShadow.transform.position = new Vector3(shadowPosition.transform.position.x, shadowPosition.transform.position.y, shadowPosition.transform.position.z);
            Destroy(shadowPosition);
            animator.SetTrigger("Land");
            hasLanded = true;
            if (targets.Count == 0) {
                animator.SetBool("isWalking", true);
                run = true;
            }
            else {
                animator.SetBool("isAttacking", true);
            }
            
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            shooting = true;
            StartTornado();
            StartCoroutine(StopRunning());
            StartCoroutine(Shoot());
            if (!targets.Contains(collision.transform))
                targets.Add(collision.transform);
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy" && hasLanded) {
            FindTarget();
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            animator.SetBool("isAttacking", false);
            run = true;
            shooting = false;
            StopTornado();
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            head.rotation = new Quaternion(0,0,0,0);
            if (targets.Contains(collision.transform)){
                targets.Remove(collision.transform);
            }
        }
    }

    IEnumerator StopRunning() {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isAttacking", true);
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

        if (distanceX < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

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
        if (shooting) {
            Quaternion finalRotation = Quaternion.AngleAxis(currentAngle - 45, Vector3.back);
            Instantiate(projectile, shotPoint.position, head.rotation);
            Recoil();
            StartCoroutine(Shoot());
        }
    }

    public void StartTornado() {
        tornadoParticles.Play();
        tornadoSound.Play();
    }

    public void StopTornado() {
        tornadoParticles.Stop();
        tornadoSound.Stop();
    }

    public void PlayLandSound() {
        fallingSound.Stop();
        landSound.Play();
    }

    private void Recoil() {
        rb2d.AddForce(Vector2.left * recoil * 100);
    }

}
