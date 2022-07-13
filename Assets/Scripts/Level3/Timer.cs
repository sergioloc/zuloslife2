using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField] private float time = 0;
    private float currentTime;
    private Image bar;

    void Start() {
        currentTime = time;
        bar = GetComponent<Image>();
        bar.fillAmount = 1f;
    }

    void Update() {
        if (currentTime > 0) {
            currentTime -= Time.deltaTime;
            bar.fillAmount = currentTime / time;
        }
        else {
            Time.timeScale = 0f;
            Destroy(gameObject);
        }
    }

}
