using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour {

    Collider2D collider;
    public bool isSelected;
    public GameObject spawn;

    void Start() {
        collider = GetComponent<Collider2D>();
        isSelected = false;
    }

    // GETTERS

    public Collider2D GetCollider() {
        return collider;
    }

    public GameObject GetSpawn() {
        return spawn;
    }

    public bool IsSelected() {
        return isSelected;
    }

    // SETTERS

    public void SetSelected(bool isSelected) {
        this.isSelected = isSelected;
    }

}
