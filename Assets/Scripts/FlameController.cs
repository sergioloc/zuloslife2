using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour {

    public float speed;
    public float lifetime;

    void Start() {
        Invoke("DestroyFlame", lifetime);
    }

    void Update() {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void DestroyFlame() {
        Destroy(gameObject);
    }

}