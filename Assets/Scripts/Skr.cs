using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skr : PlayerAction {

    [Header("Ripple Effect")]
    [SerializeField] private float duration;
    [SerializeField] private float amount = 50f;
    [Range(0,1)]
    [SerializeField] private float friction = 0.9f;
    [SerializeField] private ParticleSystem skrParticles;
    [SerializeField] private ParticleSystem shockwaveParticles;
    private Camera cam;
    private RippleEffect rippleEffect;

    [Header("Sounds")]
    [SerializeField] private AudioSource shockwaveSound;

    private Animator animator;
    private Transform mouthPosition;
    
    void Start() {
        animator = GetComponent<Animator>();
        mouthPosition = skrParticles.GetComponent<Transform>();
        GameObject[] camList = GameObject.FindGameObjectsWithTag("MainCamera");
        cam = camList[0].GetComponent<Camera>();
        rippleEffect = camList[0].GetComponent<RippleEffect>();
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

    // Listeners

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
            rippleEffect.Play(screenPos.x, screenPos.y, amount/1.5f, friction/1.5f);
        }
    }

    // Aux functions

    IEnumerator Shockwave() {
        yield return new WaitForSeconds(0.5f);
        shockwaveParticles.Play();
        skrParticles.Play();
        shockwaveSound.Play();

        Vector3 screenPos = cam.WorldToScreenPoint(mouthPosition.position);
        rippleEffect.Play(screenPos.x, screenPos.y, amount, friction);

        yield return new WaitForSeconds(duration);
        shockwaveParticles.Stop();
        rippleEffect.Stop();
        animator.SetTrigger("Cooldown");
    }

}
