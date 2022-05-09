using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cepa : PlayerAction {

    [SerializeField] private GameObject onion;
    [SerializeField] private Transform shotPoint;

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

    public override void LookAtTarget(Vector2 targetPosition) {
        
    }

    // Animation functions

    public void ThrowOnion() {
        Instantiate(onion, shotPoint.position, Quaternion.Euler(0f, 0f, -45f));
    }
    
}
