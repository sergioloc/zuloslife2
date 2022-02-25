using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamilioController : MonoBehaviour {
    
    [Header("Particles")]
    public ParticleSystem fireParticles;

    public GameObject flame;
    public float frequency;

    private bool isAttacking = false;

    IEnumerator ThrowFlame() {
        Instantiate(flame, transform.position, transform.rotation);
        yield return new WaitForSeconds(frequency);
        if (isAttacking)
            StartCoroutine(ThrowFlame());
    }

    void StartAttack() {
        isAttacking = true;
        fireParticles.Play();
        StartCoroutine(ThrowFlame());
    }

    void StopAttack() {
        isAttacking = false;
        fireParticles.Stop();
    }

}
