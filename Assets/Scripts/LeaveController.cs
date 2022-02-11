using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveController : MonoBehaviour {

    public float speed;
    public float lifetime;

    void Start() {
        Invoke("DestroyLeave", lifetime);
    }

    void Update() {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void DestroyLeave() {
        Destroy(gameObject);
    }

}
