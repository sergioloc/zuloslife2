using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerAction action;
    [SerializeField] private float actionDelay;
    [SerializeField] private string targetTag;
    [SerializeField] private List<Transform> targets;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == targetTag) {
            if (!targets.Contains(collision.transform))
                targets.Add(collision.transform);
            StartCoroutine(StopRunning());
            action.Attack();
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == targetTag) {
            FindTarget();
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == targetTag) {
            if (targets.Contains(collision.transform))
                targets.Remove(collision.transform);
            if (targets.Count == 0) {
                movement.SetRunning(true);
                action.Run();
            }
        }
    }

    private void FindTarget() {
        int index = NearestTarget();
        if (index != -1) 
            action.LookAtTarget(targets[index].position);
    }

    private int NearestTarget() {
        float[] distances = new float[targets.Count];

        for (int i = 0; i < targets.Count; i++) {
            distances[i] = (Mathf.Abs(targets[i].position.x - transform.position.x));
        }

        float minDistance = Mathf.Min(distances);
        int index = -1;

        for (int i = 0; i < distances.Length; i++) {
            if (minDistance == distances[i])
                index = i;
        }
        return index;
    }

    IEnumerator StopRunning() {
        yield return new WaitForSeconds(actionDelay);
        movement.SetRunning(false);
    }

}
