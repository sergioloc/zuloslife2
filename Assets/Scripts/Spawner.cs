using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject spawnObject;
    public Vector3 position;
    
    public void Spawn() {
        Instantiate(spawnObject, position, Quaternion.identity);
    }

}
