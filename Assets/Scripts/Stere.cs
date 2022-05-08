using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stere : PlayerAction {

    [Header("Land Effect")]
    [SerializeField] private ParticleSystem landParticles;

    // Override functions

    public override void Attack() {
       
    }

    public override void Run() {
        
    }

    public override void LookAtTarget(Vector2 targetPosition) {
        //this.targetPosition = targetPosition;
        //LookAtTarget();
    }

    // Animation functions

    public void StartLand() {
        landParticles.Play();
    }

}
