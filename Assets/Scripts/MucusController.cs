using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MucusController : MonoBehaviour {

    public GameObject particles;

    void Start() {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground") {
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
