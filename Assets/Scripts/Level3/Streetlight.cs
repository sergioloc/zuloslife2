using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streetlight : MonoBehaviour {

    [SerializeField] private float minTimeBlink;
    [SerializeField] private float maxTimeBlink;
    [SerializeField] private bool move = false;
    [SerializeField] private float speed;
    [SerializeField] private int lifetime;

    private Animator animator;

    void Start() {
        if (move)
            Invoke("DestroyStreetlight", lifetime);

        else {
            animator = GetComponent<Animator>();
            StartCoroutine(Blink(Random.Range(minTimeBlink, maxTimeBlink)));
        }
    }

    private IEnumerator Blink(float seconds) {
        yield return new WaitForSeconds(seconds);
        animator.Play("Streetlight_Blink");
        StartCoroutine(Blink(Random.Range(minTimeBlink, maxTimeBlink)));
    }

    void FixedUpdate() {
        if (move)
            transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void DestroyStreetlight() {
        Destroy(gameObject);
    }

}
