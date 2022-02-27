using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkrController : MonoBehaviour {

    public float speed;
    public float sockwaveTime;
    private UnityEngine.Rendering.Universal.UniversalAdditionalCameraData camData;
    private bool run = false;
    
    void Start() {
        GameObject[] camList = GameObject.FindGameObjectsWithTag("MainCamera");
        camData = camList[0].GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
    }

    void FixedUpdate() {
        if (run) {
            transform.Translate(Vector2.right * (speed/10) * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // Start running whem character touches ground
        if (collision.gameObject.tag == "Ground" && !run) {
            run = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Center") {
            StartCoroutine(Shockwave());
        }
    }

    IEnumerator Shockwave() {
        yield return new WaitForSeconds(1f);
        run = false;
        camData.SetRenderer(1);
        yield return new WaitForSeconds(sockwaveTime);
        camData.SetRenderer(0);
    }

}
