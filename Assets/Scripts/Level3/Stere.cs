using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stere : PlayerAction {

    [Header("Land Effect")]
    [SerializeField] private ParticleSystem landParticles;

    [Header("Sounds")]
    [SerializeField] private AudioSource getRollerSound;
    [SerializeField] private AudioSource rollerImpactSound;

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    // Override functions

    public override void Attack() {
       animator.SetBool("isAttacking", true);
    }

    public override void Run() {
        animator.SetBool("isAttacking", false);
    }

    public override void LookAt(Vector2 targetPosition) {
        
    }

    // Animation functions

    public void StartLand() {
        landParticles.Play();
    }

    public void PlayGetRoller() {
        getRollerSound.Play();
    }

    public void PlayRollerImpact() {
        rollerImpactSound.Play();
    }

}
