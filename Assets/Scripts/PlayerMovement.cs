using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField] private float speed;
    private Animator animator;
    private bool run;

    [Header("Bottom shadow")]
    public GameObject shadow;
    public GameObject shadowPosition;
    public float height = -3.52f;
    private GameObject newShadow;

    void Start() {
        run = false;
        animator = GetComponent<Animator>();
        newShadow = Instantiate(shadow, new Vector3(transform.position.x, height, transform.position.z), Quaternion.identity);
    }

    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.right * (speed / 10) * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            newShadow.transform.parent = gameObject.transform;
            newShadow.transform.position = new Vector3(shadowPosition.transform.position.x, shadowPosition.transform.position.y, shadowPosition.transform.position.z);
            Destroy(shadowPosition);
            animator.SetTrigger("Land");
            run = true;
        }
    }

    public void SetRunning(bool run) {
        this.run = run;
    }
    
}
