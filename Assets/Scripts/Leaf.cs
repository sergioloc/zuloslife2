using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour {

    public float speed;
    public float lifetime;
    public GameObject sprite;
    public GameObject particles;
    public AudioSource leaveDestroy;
    public List<AudioClip> audios;

    void Start() {
        leaveDestroy.clip = audios[Random.Range(0, audios.Count)];
        Invoke("DestroyLeave", lifetime);
    }

    void FixedUpdate() {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Instantiate(particles, transform.position, transform.rotation);
            sprite.SetActive(false);
            leaveDestroy.Play();
            StartCoroutine(DestroyLeave());
        }
    }

    IEnumerator DestroyLeave() {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
