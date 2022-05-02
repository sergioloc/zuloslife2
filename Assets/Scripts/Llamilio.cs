using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llamilio : PlayerAction {

    [Header("Fire Effect")]
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private ParticleSystem chargeParticles;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem cooldownParticles;
    [SerializeField] private GameObject flame;

    [Header("Ripple Effect")]
    [SerializeField] private float amount = 50f;
    [Range(0,1)]
    [SerializeField] private float friction = 0.9f;
    private RipplePostProcessor rippleEffect;
    private Camera cam;

    [Header("Land Effect")]
    [SerializeField] private GameObject crack;
    [SerializeField] private Transform crackPosition;

    [Header("Sounds")]
    [SerializeField] private AudioSource axeImpactSound;
    [SerializeField] private AudioSource axeBackSound;
    [SerializeField] private AudioSource gasSound;
    [SerializeField] private AudioSource flamethrowerSound;
    [SerializeField] private AudioSource cooldownSound;
    [SerializeField] private AudioSource reloadSound;

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        GameObject[] camList = GameObject.FindGameObjectsWithTag("MainCamera");
        cam = camList[0].GetComponent<Camera>();
        rippleEffect = camList[0].GetComponent<RipplePostProcessor>();
    }

    // Override functions

    public override void Attack() {
        animator.SetBool("isAttacking", true);
        gasSound.Play();
    }

    public override void Run() {
        animator.SetBool("isAttacking", false);
        fireParticles.Stop();
        flamethrowerSound.Stop();
    }

    public override void LookAtTarget(Vector2 targetPosition) {
        //this.targetPosition = targetPosition;
        //LookAtTarget();
    }

    // Animation functions

    public void Explosion() {
        chargeParticles.Stop();
        explosionParticles.Play();
    }

    public void Crack() {
        Instantiate(crack, crackPosition.position, crackPosition.rotation);
    }

    public void PlayAxeImpact() {
        axeImpactSound.Play();
    }

    public void PlayAxeBack() {
        axeBackSound.Play();
    }

    public void PlayReload() {
        reloadSound.Play();
    }

    public void StartFire() {
        fireParticles.Play();
        flamethrowerSound.Play();
    }

    public void Cooldown() {
        animator.SetTrigger("Cooldown");
        cooldownParticles.Play();
        cooldownSound.Play();
        fireParticles.Stop();
        flamethrowerSound.Stop();

        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        rippleEffect.Play(screenPos.x, screenPos.y, amount, friction);
    }

}
