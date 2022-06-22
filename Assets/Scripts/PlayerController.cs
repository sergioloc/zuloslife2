using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerAction action;
    public bool isFemale;

    [Space()]
    [SerializeField] private List<string> targetsTag;
    [SerializeField] private List<Transform> targets;
    private string targetTag;

    public void OnAreaTriggerEnter(OnTriggerDelegation delegation) {
        if (isEnemy(delegation.Other.gameObject.tag)) {
            if (!targets.Contains(delegation.Other.transform)) {
                targets.Add(delegation.Other.transform);
                Debug.Log("Attack");
                movement.SetRunning(false);
                action.Attack();
            }
        }
    }

    public void OnAreaTriggerStay(OnTriggerDelegation delegation) {
        if (isEnemy(delegation.Other.gameObject.tag)) {
            FindTarget();
        }
    }

    public void OnAreaTriggerExit(OnTriggerDelegation delegation) {
        if (isEnemy(delegation.Other.gameObject.tag)) {
            targets.Remove(delegation.Other.transform);
            if (targets.Count == 0) {
                Debug.Log("Run");
                movement.SetRunning(true);
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
