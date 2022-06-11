using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroController : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private int wallIn;
    
    private bool run;
    private Animator animator;

    void Start() {
        run = true;
        animator = GetComponent<Animator>();
        StartCoroutine(StartWall(Random.Range(wallIn, wallIn + 5)));
    }

    
    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.left * (speed/10) * Time.deltaTime);
        }
    }

    private IEnumerator StartWall(float seconds) {
        yield return new WaitForSeconds(seconds);
        run = false;
        animator.SetBool("isWall", true);
    }
    
}
