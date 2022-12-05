using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour {

    [SerializeField] private GameObject building;
    [SerializeField] private float frecuency;

    void Start() {
        Instantiate(building, transform.position, Quaternion.identity);
        StartCoroutine(CreateBuilding());
    }

    private IEnumerator CreateBuilding() {
        yield return new WaitForSeconds(frecuency);
        Instantiate(building, transform.position, Quaternion.identity);
        StartCoroutine(CreateBuilding());
    }

}
