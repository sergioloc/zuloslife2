using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkrController : MonoBehaviour {

    public float speed;
    public float sockwaveTime;
    public GameObject skrParticles;
    private Camera cam;
    private RipplePostProcessor rippleEffect;

    [Header("Ripple Effect")]
    [SerializeField] private float amount = 50f;
    [Range(0,1)]
    [SerializeField] private float friction = 0.9f;

    private bool run = false;
    
    void Start() {
        GameObject[] camList = GameObject.FindGameObjectsWithTag("MainCamera");
        cam = camList[0].GetComponent<Camera>();
        rippleEffect = camList[0].GetComponent<RipplePostProcessor>();
    }

    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.right * (speed/10) * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // Start running whem character touches ground
        if (collision.gameObject.tag == "Ground") {
            run = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Kasper") {
            run = false;
            StartCoroutine(Shockwave());
        }
    }

    IEnumerator Shockwave() {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        rippleEffect.Play(screenPos.x, screenPos.y, amount, friction);
        yield return new WaitForSeconds(3f);
        rippleEffect.Stop();
    }

}
