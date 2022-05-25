using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField] private float speed;
    [SerializeField] private float runDelay;
    [SerializeField] private bool reverse = false;
    [SerializeField] private GameObject landDetector;

    [Header("Sounds")]
    [SerializeField] private AudioSource fallingSound;
    [SerializeField] private AudioSource landSound;
    [SerializeField] private AudioSource stepSound;
    [SerializeField] private List<AudioClip> steps;

    [Header("Bottom shadow")]
    [SerializeField] private GameObject shadow;
    [SerializeField] private GameObject shadowPosition;
    [SerializeField] private float height = -3.52f;
    [SerializeField] private bool bounce = false;
    private GameObject newShadow;

    private Animator animator;
    private bool run;

    void Start() {
        run = false;
        animator = GetComponent<Animator>();
        newShadow = Instantiate(shadow, new Vector3(transform.position.x, height, transform.position.z), Quaternion.identity);
    }

    void FixedUpdate() {
        if (run) {
            if (reverse)
                transform.Translate(Vector2.left * (speed / 10) * Time.deltaTime);
            else
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
            StartCoroutine(StartRunning());
        }
    }

    IEnumerator StartRunning() {
        yield return new WaitForSeconds(runDelay);
        run = true;
        if (bounce)
            newShadow.GetComponent<Animator>().SetTrigger("Bounce");
    }

    public void SetRunning(bool run) {
        this.run = run;
    }

    private void PlayLandSound() {
        if (fallingSound) fallingSound.Stop();
        if (landSound) landSound.Play();
    }

    public void PlayStep() {
        if (stepSound) {
            stepSound.clip = steps[Random.Range(0, steps.Count-1)];
            stepSound.Play();
        }
    }
    
}
