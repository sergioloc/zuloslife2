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
                Debug.Log(hit.collider);
                if (hit.collider == spawnArea) {
                    CardController activeCard = GetActiveCard();
                    if (activeCard != null) {
                        Instantiate(activeCard.GetSpawn(), mousePos2D, Quaternion.identity);
                        activeCard.SetSelected(false);
                        activeCard.Cooldown();
                    }
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
        if (cards[0].IsReady()) {
            cards[0].SetSelected(true);
            cards[1].SetSelected(false);
            cards[2].SetSelected(false);
            cards[3].SetSelected(false);
            cards[4].SetSelected(false);
        }
    }

    public void pressCard2() {
        if (cards[1].IsReady()) {
            cards[0].SetSelected(false);
            cards[1].SetSelected(true);
            cards[2].SetSelected(false);
            cards[3].SetSelected(false);
            cards[4].SetSelected(false);
        }
    }

    public void pressCard3() {
        if (cards[2].IsReady()) {
            cards[0].SetSelected(false);
            cards[1].SetSelected(false);
            cards[2].SetSelected(true);
            cards[3].SetSelected(false);
            cards[4].SetSelected(false);
        }
    }

    public void pressCard4() {
        if (cards[3].IsReady()) {
            cards[0].SetSelected(false);
            cards[1].SetSelected(false);
            cards[2].SetSelected(false);
            cards[3].SetSelected(true);
            cards[4].SetSelected(false);
        }
    }

    public void pressCard5() {
        if (cards[4].IsReady()) {
            cards[0].SetSelected(false);
            cards[1].SetSelected(false);
            cards[2].SetSelected(false);
            cards[3].SetSelected(false);
            cards[4].SetSelected(true);
        }
    }

}
