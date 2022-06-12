using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muro : MonoBehaviour {

    [Header("Walk")]
    [SerializeField] private float speed;
    [SerializeField] private AudioSource stepSound;
    [SerializeField] private List<AudioClip> steps;

    [Header("Attack")]
    [SerializeField] private ParticleSystem punchParticles;
    [SerializeField] private AudioSource punchSound;

    [Header("Ripple Effect")]
    [SerializeField] private float amount = 50f;
    [Range(0,1)]
    [SerializeField] private float friction = 0.9f;
    [SerializeField] private Transform punch;
    private RippleEffect rippleEffect;
    private Camera cam;

    private bool run;
    private bool canMove;
    private Animator animator;

    void Start() {
        run = true;
        canMove = true;
        animator = GetComponent<Animator>();
        GameObject[] camList = GameObject.FindGameObjectsWithTag("MainCamera");
        cam = camList[0].GetComponent<Camera>();
        rippleEffect = camList[0].GetComponent<RippleEffect>();
    }

    
    void FixedUpdate() {
        if (run && canMove) {
            transform.Translate(Vector2.left * (speed/10) * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Attack();
        }
        else if (collision.gameObject.tag == "Shield") {
            canMove = false;
            Attack();
        }
    }

    private void Attack() {
        run = false;
        animator.SetBool("isAttacking", true);
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

        Vector3 screenPos = cam.WorldToScreenPoint(punch.position);
        rippleEffect.Play(screenPos.x, screenPos.y, amount, friction);
    }

    public void OnAttackFinish() {
        animator.SetBool("isAttacking", false);
        if (!canMove)
            Attack();
        else
            run = true;
    }
    
}
