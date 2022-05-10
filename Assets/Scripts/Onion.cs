using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onion : MonoBehaviour {
    
    [SerializeField] private float lifetime;
    [SerializeField] private GameObject explosionParticles;
    [HideInInspector] public float force;
    private Rigidbody2D rb2D;

    void Start() {
        Invoke("DestroyOnion", lifetime);
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.AddForce(transform.up * force * 10);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            Instantiate(explosionParticles, transform.position, transform.rotation);
            DestroyOnion();
        }
    }

    public void DestroyOnion() {
        Destroy(gameObject);
    }
    
}
