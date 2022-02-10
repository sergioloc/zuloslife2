using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour {

    public List<CardController> cards;
    Collider2D spawnArea;

    void Start() {
        spawnArea = GetComponent<Collider2D>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                if (hit.collider == spawnArea) {
                    CardController activeCard = GetActiveCard();
                    Instantiate(activeCard.GetSpawn(), mousePos2D, Quaternion.identity);
                }
                else {
                    for (int i = 0; i < cards.Count; i++) {
                        if (hit.collider == cards[i].GetCollider())
                            cards[i].SetSelected(true);
                        else
                            cards[i].SetSelected(false);
                    }
                }
            }
        }
    }

    private CardController GetActiveCard() {
        for(int i = 0; i < cards.Count; i++) {
            if (cards[i].IsSelected())
                return cards[i];
        }
        return null;
    }

    public void pressCard1() {
        cards[0].SetSelected(true);
        cards[1].SetSelected(false);
        cards[2].SetSelected(false);
        cards[3].SetSelected(false);
        cards[4].SetSelected(false);
    }

    public void pressCard2() {
        cards[0].SetSelected(false);
        cards[1].SetSelected(true);
        cards[2].SetSelected(false);
        cards[3].SetSelected(false);
        cards[4].SetSelected(false);
    }

    public void pressCard3() {
        cards[0].SetSelected(false);
        cards[1].SetSelected(false);
        cards[2].SetSelected(true);
        cards[3].SetSelected(false);
        cards[4].SetSelected(false);
    }

    public void pressCard4() {
        cards[0].SetSelected(false);
        cards[1].SetSelected(false);
        cards[2].SetSelected(false);
        cards[3].SetSelected(true);
        cards[4].SetSelected(false);
    }

    public void pressCard5() {
        cards[0].SetSelected(false);
        cards[1].SetSelected(false);
        cards[2].SetSelected(false);
        cards[3].SetSelected(false);
        cards[4].SetSelected(true);
    }

}
