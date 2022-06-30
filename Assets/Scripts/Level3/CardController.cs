using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour {

    [SerializeField] private GameObject spawn;
    [SerializeField] private Image loader;
    [SerializeField] private int cooldown = 0;
    [SerializeField] private bool isSelected;
    [SerializeField] private Image front;
    [SerializeField] private Sprite spriteSelected;
    [SerializeField] private Sprite spriteUnselected;

    private Animator animator;
    private float counter;
    private bool isCooldown;

    void Start() {
        loader.fillAmount = 0f;
        counter = 0;
        isCooldown = false;
        isSelected = false;
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (isCooldown) {
            loader.fillAmount = counter / cooldown;
            counter -= Time.deltaTime;
            if (loader.fillAmount == 0f)
                isCooldown = false;
        }
    }

    // GETTERS

    public Collider2D GetCollider() {
        return GetComponent<Collider2D>();
    }

    public GameObject GetSpawn() {
        return spawn;
    }

    public bool IsReady() {
        return counter <= 0;
    }

    public bool IsSelected() {
        return isSelected;
    }

    // SETTERS

    public void SetSelected(bool isSelected) {
        this.isSelected = isSelected;
        animator.SetBool("isSelected", isSelected);
        if (isSelected)
            front.sprite = spriteSelected;
        else
            front.sprite = spriteUnselected;
    }

    public void Cooldown() {
        if (cooldown != 0) {
            isCooldown = true;
            counter = cooldown;
            loader.fillAmount = 1f;
        }
    }

}
