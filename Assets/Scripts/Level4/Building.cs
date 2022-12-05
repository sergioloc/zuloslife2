using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random=UnityEngine.Random;

public class Building : MonoBehaviour {
    
    [SerializeField] private int speed;
    [SerializeField] private int lifetime;
    [SerializeField] private SpriteRenderer windowsRenderer;
    [SerializeField] private BuildingInfo[] types;
    [SerializeField] private Color[] colors;
    private SpriteRenderer spriteRenderer;

    [Serializable]
    private class BuildingInfo {
        public Sprite sprite;
        public Sprite[] windows;
    }

    void Start() {
        Invoke("DestroyBuilding", lifetime);
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Select building type
        int typePos = Random.Range(0, types.Length);
        spriteRenderer.sprite = types[typePos].sprite;
        spriteRenderer.color = colors[Random.Range(0, colors.Length)];

        // Select windows type
        int windowPos = Random.Range(0, types[typePos].windows.Length);
        windowsRenderer.sprite = types[typePos].windows[windowPos];
    }

    void FixedUpdate() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void DestroyBuilding() {
        Destroy(gameObject);
    }

}
