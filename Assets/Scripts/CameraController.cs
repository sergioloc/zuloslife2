using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject[] cameras;
    private Animator animator;

    void Start() {
        cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        animator = cameras[0].GetComponent<Animator>();
    }

    public void Shake() {
        animator.SetTrigger("Shake");
    }

}
