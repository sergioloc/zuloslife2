using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    [SerializeField] private float health;
    [SerializeField] private bool isEnemy;

    [Header("Character")]
    [SerializeField] private float fire; // Llamilio
    [SerializeField] private float leaf; // GreenLife
    [SerializeField] private float roller; // Stere
    [SerializeField] private float onion; // Cepa
    [SerializeField] private float shockwave; // Skr

    [Header("Enemy")]
    [SerializeField] private float mucus; // Moquino
    [SerializeField] private float stone; // Muro
    [SerializeField] private float kasper; // Kasper

    void OnTriggerEnter2D(Collider2D collision) {
        if (isEnemy) {
            switch(collision.gameObject.tag) {
                case "Fire":
                health -= fire;
                break;

                case "Leaf":
                health -= leaf;
                break;
                
                case "Roller":
                health -= roller;
                break;

                case "Onion":
                health -= onion;
                break;

                case "Shockwave":
                health -= shockwave;
                break;
            }
        }
        else {
            switch(collision.gameObject.tag) {
                case "Mucus":
                health -= mucus;
                break;

                case "Stone":
                health -= stone;
                break;

                case "Kasper":
                health -= kasper;
                break;
            }
        }

        if (health <= 0)
            Destroy(gameObject);

        //collision.gameObject.GetComponent<Animator>().SetTrigger("Absorb");
    }


}
