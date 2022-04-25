using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour {

    [SerializeField] private float time;

    void Start() {
        Invoke("DestroyObject", time);
    }

    public void DestroyObject() {
        Destroy(gameObject);
    }
    
}
