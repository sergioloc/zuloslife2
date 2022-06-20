using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    [SerializeField] private float interval;
    [SerializeField] private List<string> animations;

    [Header("Spawner")]
    [SerializeField] private GameObject thug;
    [SerializeField] private Transform top;
    [SerializeField] private Transform middle;
    [SerializeField] private Transform bottom;

    private Animator animator;
    private string last = "";

    void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine(ChangeLight(Random.Range(interval, interval * 2)));
    }

    private IEnumerator ChangeLight(float seconds) {
        yield return new WaitForSeconds(seconds);
        animator.Play(animations[Random.Range(0, animations.Count)]);
        StartCoroutine(ChangeLight(Random.Range(interval, interval * 2)));
    }

    public void SpawnTop() {
        if (top && last != "top") {
            last = "top";
            StartCoroutine(Spawn(top.position));
        }
    }

    public void SpawnMiddle() {
        if (middle && last != "middle") {
            last = "middle";
            StartCoroutine(Spawn(middle.position));
        }
    }
    
    public void SpawnBottom() {
        if (bottom && last != "bottom") {
            last = "bottom";
            StartCoroutine(Spawn(bottom.position));
        }
    }

    public void Reset() {
        last = "";
    }

    private IEnumerator Spawn(Vector3 position) {
        yield return new WaitForSeconds(2f);
        Instantiate(thug, position, transform.rotation);
    }

}
