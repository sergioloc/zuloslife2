using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llamilio : PlayerAction {
    
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private ParticleSystem chargeParticles;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private GameObject flame;
    [SerializeField] private GameObject crack;
    [SerializeField] private Transform crackPosition;
    [SerializeField] private float frequency;
    private bool isAttacking = false;

    // Override functions

    public override void Attack() {
        isAttacking = true;
        fireParticles.Play();
        StartCoroutine(ThrowFlame());
    }

    public override void Run() {
        isAttacking = false;
        fireParticles.Stop();
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
    

    IEnumerator ThrowFlame() {
        Instantiate(flame, transform.position, transform.rotation);
        yield return new WaitForSeconds(frequency);
        if (isAttacking)
            StartCoroutine(ThrowFlame());
    }

}
