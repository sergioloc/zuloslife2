using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkrController : MonoBehaviour {

    public UnityEngine.Rendering.Universal.UniversalAdditionalCameraData cam;
    public int newIndex;
    
    void Start() {
        StartCoroutine(StopRunning());
    }

    
    IEnumerator StopRunning() {
        yield return new WaitForSeconds(2f);
        cam.SetRenderer(newIndex);
    }

}
