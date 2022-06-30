using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAction : MonoBehaviour {

    public abstract void Attack();

    public abstract void Run();

    public abstract void LookAt(Vector2 targetPosition);

}
