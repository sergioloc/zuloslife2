using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour {

    public bool isSelected;
    public GameObject spawn;

    void Start() {
        isSelected = false;
    }

    // GETTERS

    public Collider2D GetCollider() {
        return GetComponent<Collider2D>();
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
