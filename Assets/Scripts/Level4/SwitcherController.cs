using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherController : MonoBehaviour {
    
    [SerializeField] private float shrinkScale;
    [SerializeField] private float shrinkDuration;
    [SerializeField] private float shrinkSpeed;

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

    private Animator animator;
   private bool isShrinking = false;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
      // Jota
      if (Input.GetKeyDown(KeyCode.Q)) {
         changeTo(0);
         animator.SetTrigger("Dance");
      }
      // Natalia
      else if (Input.GetKeyDown(KeyCode.W)) {
         changeTo(1);
         animator.SetTrigger("Vomit");
      }
      // Isma
      else if (Input.GetKeyDown(KeyCode.E)) {
         changeTo(2);
         animator.SetTrigger("Whip");
      } 
      // Sandra
      else if (Input.GetKeyDown(KeyCode.R)) {
         changeTo(3);
         animator.SetTrigger("Invoke");
      }
      // Pablo
      else if (Input.GetKeyDown(KeyCode.T)) {
         changeTo(4);
         if (!isShrinking) {
            StartCoroutine(Shrink());
         }
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

    private IEnumerator Shrink() {
      isShrinking = true;
      Vector3 originalScale = transform.localScale;
      Vector3 targetScale = originalScale * shrinkScale;

      float currentTime = 0f;

      while (currentTime < shrinkDuration) {
         transform.localScale = Vector3.Lerp(originalScale, targetScale, currentTime * shrinkSpeed / shrinkDuration);
         currentTime += Time.deltaTime;
         yield return null;
      }

      transform.localScale = targetScale;

      yield return new WaitForSeconds(shrinkDuration);

      currentTime = 0f;

      while (currentTime < shrinkDuration)
      {
         transform.localScale = Vector3.Lerp(targetScale, originalScale, currentTime * shrinkSpeed / shrinkDuration);
         currentTime += Time.deltaTime;
         yield return null;
      }

      transform.localScale = originalScale;

      isShrinking = false;
    }

}
