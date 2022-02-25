using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamilioController : MonoBehaviour {
    
    [Header("Particles")]
    public ParticleSystem fireParticles;
    public GameObject damage;

    void StartAttack() {
        Debug.Log("Fire!");
        fireParticles.Play();
        damage.SetActive(true);
    }

    void StopAttack() {
        Debug.Log("Cooldown...");
        fireParticles.Stop();
        damage.SetActive(false);
    }

}
