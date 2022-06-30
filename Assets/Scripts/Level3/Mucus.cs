using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mucus : MonoBehaviour {

    [SerializeField] private GameObject particles;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground") {
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
