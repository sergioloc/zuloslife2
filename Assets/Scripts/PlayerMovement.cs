using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField] private float speed;
    [SerializeField] private GameObject landDetector;

    [Header("Sounds")]
    [SerializeField] private AudioSource fallingSound;
    [SerializeField] private AudioSource landSound;

    private Animator animator;
    private bool run;

    [Header("Bottom shadow")]
    public GameObject shadow;
    public GameObject shadowPosition;
    public float height = -3.52f;
    private GameObject newShadow;

    void Start() {
        run = false;
        animator = GetComponent<Animator>();
        newShadow = Instantiate(shadow, new Vector3(transform.position.x, height, transform.position.z), Quaternion.identity);
    }

    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.right * (speed / 10) * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Ground") {
            animator.SetTrigger("Land");
            Destroy(landDetector);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            PlayLandSound();
            newShadow.transform.parent = gameObject.transform;
            newShadow.transform.position = new Vector3(shadowPosition.transform.position.x, shadowPosition.transform.position.y, shadowPosition.transform.position.z);
            Destroy(shadowPosition);
            run = true;
        }
    }

    public void SetRunning(bool run) {
        this.run = run;
    }

    private void PlayLandSound() {
        fallingSound.Stop();
        landSound.Play();
    }
    
}
