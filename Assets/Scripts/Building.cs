using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    [SerializeField] private float interval;
    [SerializeField] private List<string> animations;

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine(ChangeLight(Random.Range(interval, interval * 2)));
    }

    private IEnumerator ChangeLight(float seconds) {
        yield return new WaitForSeconds(seconds);
        animator.Play(animations[Random.Range(0, animations.Count)]);
        StartCoroutine(ChangeLight(Random.Range(interval, interval * 2)));
    }

}
