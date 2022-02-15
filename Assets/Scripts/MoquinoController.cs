using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoquinoController : MonoBehaviour {

    public float speed;
    public GameObject mucus;
    public Transform shotPoint;

    private bool run;
    private Animator animator;

    void Start() {
        run = true;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack() {
        yield return new WaitForSeconds(1.5f);
        run = false;
        animator.SetTrigger("Attack");
        
        yield return new WaitForSeconds(1.15f);

        Instantiate(mucus, shotPoint.position, transform.rotation);

        yield return new WaitForSeconds(0.5f);
        run = true;
    }

}
