using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsGenerator : MonoBehaviour {

    [SerializeField] private float interval;
    [SerializeField] private float maxHigh;
    [SerializeField] private float minHigh;
    [SerializeField] private GameObject cloud;
    
    void Start() {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate() {
        Vector3 position = new Vector3(transform.position.x, Random.Range(minHigh, maxHigh), 0);
        Instantiate(cloud, position, transform.rotation);
        yield return new WaitForSeconds(Random.Range(interval, interval * 1.5f));

        StartCoroutine(Generate());
    }

}
