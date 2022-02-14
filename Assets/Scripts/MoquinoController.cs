using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoquinoController : MonoBehaviour {

    public float speed;
    public int targets;

    private bool run;

    void Start() {
        targets = 0;
        run = true;
    }

    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            targets++;
            StartCoroutine(StopRunning());
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            targets--;
            if (targets < 1){
                run = true;
            }
        }
    }

    IEnumerator StopRunning() {
        yield return new WaitForSeconds(1f);
        run = false;
    }

}
