using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streetlight : MonoBehaviour {

    [SerializeField] private float minTimeBlink;
    [SerializeField] private float maxTimeBlink;

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine(Blink(Random.Range(minTimeBlink, maxTimeBlink)));
    }

    private IEnumerator Blink(float seconds) {
        yield return new WaitForSeconds(seconds);
        animator.Play("Streetlight_Blink");
        StartCoroutine(Blink(Random.Range(minTimeBlink, maxTimeBlink)));
    }

}
