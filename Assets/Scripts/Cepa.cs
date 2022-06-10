using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cepa : PlayerAction {

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shotPoint;
    private float distance = 0;
    private float angle = 0;

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
        distance = targetPosition.x - transform.position.x;
        angle = -30f;
        if (distance < 0) {
            distance = Mathf.Abs(distance);
            angle = Mathf.Abs(angle);
        }
    }

    // Animation functions

    public void ThrowOnion() {
        GameObject onion = Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, angle));
        Debug.Log(distance);
        onion.GetComponent<Onion>().force = getForce();
    
    }

    private float getForce() {
        if (distance > 10)
            return distance * 4.75f;
        else if (distance > 8)
            return distance * 5.25f;
        else if (distance > 7)
            return distance * 5.5f;
        else if (distance > 6)
            return distance * 5.75f;
        else if (distance > 5)
            return distance * 6f;
        else
            return distance * 7f;
    }
    
}
