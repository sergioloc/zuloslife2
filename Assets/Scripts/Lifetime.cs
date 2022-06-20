using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour {

    [SerializeField] private float time;
    [SerializeField] private bool destroyer;

    void Start() {
        if (time != 0)
            Invoke("DestroyObject", time);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (destroyer && collision.gameObject.tag == "Destroyer") {
            DestroyObject();
        }
    }

    public void DestroyObject() {
        Destroy(gameObject);
    }
    
}
