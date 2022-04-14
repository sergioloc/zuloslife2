using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThugSpawner : MonoBehaviour {

    public float cooldown;
    public GameObject thug;

    void Start() {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(cooldown);
        Instantiate(thug, transform.position, transform.rotation);
    }

}
