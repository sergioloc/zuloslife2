using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherController : MonoBehaviour {
    
    [SerializeField] private SpriteRenderer head;
    [SerializeField] private SpriteRenderer shoulderL;
    [SerializeField] private SpriteRenderer shoulderR;
    [SerializeField] private SpriteRenderer armL;
    [SerializeField] private SpriteRenderer armR;
    [SerializeField] private SpriteRenderer handL;
    [SerializeField] private SpriteRenderer handR;
    [SerializeField] private SpriteRenderer chest;
    [SerializeField] private SpriteRenderer pants;
    [SerializeField] private SpriteRenderer legL;
    [SerializeField] private SpriteRenderer legR;
    [SerializeField] private SpriteRenderer footL;
    [SerializeField] private SpriteRenderer footR;

    [SerializeField] private List<Character> characters;

    void Start() {
        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
           changeTo(0);
        }
        else if (Input.GetKeyDown(KeyCode.W)) {
           changeTo(1);
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
           changeTo(2);
        }
        else if (Input.GetKeyDown(KeyCode.R)) {
           changeTo(3);
        }
        else if (Input.GetKeyDown(KeyCode.T)) {
           changeTo(4);
        }
    }

    private void changeTo(int position) {
        head.sprite = characters[position].head;
        shoulderL.sprite = characters[position].shoulder;
        shoulderR.sprite = characters[position].shoulder;
        armL.sprite = characters[position].arm;
        armR.sprite = characters[position].arm;
        handL.sprite = characters[position].hand;
        handR.sprite = characters[position].hand;
        chest.sprite = characters[position].chest;
        pants.sprite = characters[position].pants;
        legL.sprite = characters[position].leg;
        legR.sprite = characters[position].leg;
        footL.sprite = characters[position].foot;
        footR.sprite = characters[position].foot;
    }

}
