using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target {
    
    public Transform transform;
    public string tag;

    public Target(Transform transform, string tag) {
        this.transform = transform;
        this.tag = tag;
    }

    public string toString() {
        return tag + ", " + transform.position.x;
    }

}
