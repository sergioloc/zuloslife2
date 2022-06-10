using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerAction action;
    [SerializeField] private float actionDelay;

    [Space()]
    [SerializeField] private List<string> targetsTag;
    [SerializeField] private List<Transform> targets;
    private string targetTag;

    void OnTriggerEnter2D(Collider2D collision) {
        if (isEnemy(collision.gameObject.tag)) {
            if (!targets.Contains(collision.transform))
                targets.Add(collision.transform);
            StartCoroutine(StopRunning());
            action.Attack();
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (isEnemy(collision.gameObject.tag)) {
            FindTarget();
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (isEnemy(collision.gameObject.tag)) {
            if (targets.Contains(collision.transform))
                targets.Remove(collision.transform);
            if (targets.Count == 0) {
                movement.SetRunning(true);
                movement.ResetLook();
                action.Run();
            }
        }
    }

    public void ReverseTag() {
        targetsTag = new List<string>();
        targetsTag.Add("Shield");
        targetsTag.Add("Player");
        targetTag = targetsTag[0];
    }

    
    IEnumerator StopRunning() {
        yield return new WaitForSeconds(actionDelay);
        movement.SetRunning(false);
    }

    // Get target enemy from area
    private void FindTarget() {
        int index = -1;

        for (int i = 0; i < targets.Count; i++) {
            if (targets[i].tag == targetTag) {
                index = i;
            }
        }

        // If there are no enemies in area, get nearest enemy
        if (index == -1)
            index = NearestTarget();

        if (index != -1) {
            movement.LookAt(targets[index].position);
            action.LookAt(targets[index].position);
        }
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

    // Check if tag belongs to an enemy
    private bool isEnemy(string tag) {
        if (targetTag == tag)
            return true;
        else {
            for (int i = 0; i < targetsTag.Count; i++) {
                if (targetsTag[i] == tag)
                    return true;
            }
        }
        return false;
    }

}
