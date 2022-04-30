using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llamilio : PlayerAction {

    [Header("Effects")]
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private ParticleSystem chargeParticles;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem cooldownParticles;
    [SerializeField] private GameObject flame;
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
    }

}
