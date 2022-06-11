using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroController : MonoBehaviour {

    [Header("Walk")]
    [SerializeField] private float speed;
    [SerializeField] private AudioSource stepSound;
    [SerializeField] private List<AudioClip> steps;

    [Header("Attack")]
    [SerializeField] private int interval;
    [SerializeField] private ParticleSystem punchParticles;
    [SerializeField] private AudioSource punchSound;

    private bool run;
    private Animator animator;

    void Start() {
        run = true;
        animator = GetComponent<Animator>();
        StartCoroutine(Attack());
    }

    
    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.left * (speed/10) * Time.deltaTime);
        }
    }

    private IEnumerator StartWall(float seconds) {
        yield return new WaitForSeconds(seconds);
        run = false;
        animator.SetBool("isWall", true);
    }

    private IEnumerator Attack() {
        yield return new WaitForSeconds(interval);
        run = false;
        animator.SetTrigger("Attack");
    }

    public void PlayStep() {
        if (stepSound) {
            stepSound.clip = steps[Random.Range(0, steps.Count)];
            stepSound.Play();
        }
    }

    public void PlayPunch() {
        punchParticles.Play();
        punchSound.Play();
    }

    public void OnAttackFinish() {
        run = true;
        StartCoroutine(Attack());
    }
    
}
