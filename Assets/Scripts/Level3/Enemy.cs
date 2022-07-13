using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy {
    
    private string name;
    private GameObject prefab;
    private Vector3 position;
    private bool isAvailable;

    public Enemy(string name, GameObject prefab, Vector3 position) {
        this.name = name;
        this.prefab = prefab;
        this.position = position;
    }

    // Getters

    public string getName() {
        return name;
    }

    public GameObject getPrefab() {
        return prefab;
    }

    public Vector3 getPosition() {
        return position;
    }

    public bool IsAvailable() {
        return isAvailable;
    }
    
}
