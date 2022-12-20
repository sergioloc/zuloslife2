using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour {

    [SerializeField] private GameObject building;
    [SerializeField] private float buildingFrecuency;
    [SerializeField] private Transform buildingSpawnPoint;
    [SerializeField] private GameObject streetlight;
    [SerializeField] private float streetlightFrecuency;
    [SerializeField] private Transform streetlightSpawnPoint;

    void Start() {
        Instantiate(building, buildingSpawnPoint.position, Quaternion.identity);
        StartCoroutine(CreateBuilding());
        StartCoroutine(CreateStreetlight());
    }

    private IEnumerator CreateBuilding() {
        yield return new WaitForSeconds(buildingFrecuency);
        Instantiate(building, buildingSpawnPoint.position, Quaternion.identity);
        StartCoroutine(CreateBuilding());
    }

    private IEnumerator CreateStreetlight() {
        yield return new WaitForSeconds(streetlightFrecuency);
        Instantiate(streetlight, streetlightSpawnPoint.position, Quaternion.identity);
        StartCoroutine(CreateStreetlight());
    }

}
