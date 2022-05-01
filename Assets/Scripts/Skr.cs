using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skr : PlayerAction {

    [Header("Ripple Effect")]
    [SerializeField] private float amount = 50f;
    [Range(0,1)]
    [SerializeField] private float friction = 0.9f;
    [SerializeField] private GameObject skrParticles;
    [SerializeField] private ParticleSystem shockwaveParticles;
    private Camera cam;
    private RipplePostProcessor rippleEffect;

    [Header("Sounds")]
    [SerializeField] private AudioSource shockwaveSound;

    private Animator animator;
    
    void Start() {
        //animator = GetComponent<Animator>();
        GameObject[] camList = GameObject.FindGameObjectsWithTag("MainCamera");
        cam = camList[0].GetComponent<Camera>();
        rippleEffect = camList[0].GetComponent<RipplePostProcessor>();
    }

    // Override functions

    public override void Attack() {
        StartCoroutine(Shockwave());
    }

    public override void Run() {
        //animator.SetBool("isAttacking", false);
        //fireParticles.Stop();
        //flamethrowerSound.Stop();
    }

    public override void LookAtTarget(Vector2 targetPosition) {
        //this.targetPosition = targetPosition;
        //LookAtTarget();
    }

    // Aux functions

    IEnumerator Shockwave() {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        rippleEffect.Play(screenPos.x, screenPos.y, amount, friction);
        yield return new WaitForSeconds(3f);
        rippleEffect.Stop();
    }

}
