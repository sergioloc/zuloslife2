using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour {

    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject landDetector;

    private Rigidbody2D rb2d;
    private Animator animator;
    private bool isGrounded;
    
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isGrounded = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
           rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
           animator.SetBool("isJumping", true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground" && !isGrounded) {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground" && isGrounded) {
            isGrounded = false;
        }
    }

}
