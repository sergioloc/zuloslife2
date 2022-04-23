using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAction : MonoBehaviour {

    public abstract void Attack();

    public abstract void Run();

    public abstract void SetTargetPosition(Vector2 targetPosition);

}
