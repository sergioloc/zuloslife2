using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLife : PlayerAction {

    [Header("Attack")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float recoil;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform head;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private ParticleSystem tornadoParticles;
    [SerializeField] private ParticleSystem chargeParticles;

    [Header("Sounds")]
    [SerializeField] private AudioSource tornadoSound;

    private Rigidbody2D rb2d;
    private Animator animator;
    private bool shooting = false;
    private float currentAngle = 0;

    void Start() {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }    

    // Override functions

    public override void Attack() {
        animator.SetBool("isAttacking", true);
        shooting = true;
        StartTornado();
        StartCoroutine(Shoot());
    }

    public override void Run() {
        animator.SetBool("isAttacking", false);
        shooting = false;
        StopTornado();
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        head.rotation = new Quaternion(0,0,0,0);
    }

    public override void LookAtTarget(Vector2 targetPosition) {
        float distanceX = targetPosition.x - transform.position.x;
        float distanceY = targetPosition.y - transform.position.y;

        if (distanceX < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        currentAngle = Mathf.Atan2(distanceX, distanceY) * Mathf.Rad2Deg;
        Quaternion endRotation = Quaternion.AngleAxis(currentAngle, Vector3.back);
        head.rotation = Quaternion.Slerp(head.rotation, endRotation, Time.deltaTime * rotateSpeed);
    }

    // Custom functions

    IEnumerator Shoot() {
        yield return new WaitForSeconds(1f);
        if (shooting) {
            Quaternion finalRotation = Quaternion.AngleAxis(currentAngle - 45, Vector3.back);
            Instantiate(projectile, shotPoint.position, head.rotation);
            Recoil();
            StartCoroutine(Shoot());
        }
    }

    // Animation functions

    public void StartCharge() {
        rb2d.AddForce(Vector2.up * 700);
        chargeParticles.Play();
    }

    public void StartTornado() {
        chargeParticles.Play();
        tornadoParticles.Play();
        tornadoSound.Play();
    }

    public void StopTornado() {
        tornadoParticles.Stop();
        tornadoSound.Stop();
    }

    private void Recoil() {
        rb2d.AddForce(Vector2.left * recoil * 10);
    }

    public void StopRecoil() {
        shooting = false;
        rb2d.velocity = Vector3.zero;
        rb2d.angularVelocity = 0f;
    }

}
