using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float interval;
    public GameObject moquino;
    public GameObject kasper;
    public GameObject muro;
    public GameObject rose;
    public GameObject pb;

    private List<Enemy> enemies;
    private string lastEnemy = "";
    
    void Start() {
        enemies = new List<Enemy>();
        enemies.Add(new Enemy("Moquino", moquino, new Vector3(7, 3, 0)));
        enemies.Add(new Enemy("Kasper", kasper, new Vector3(7, -3, 0)));
        enemies.Add(new Enemy("Muro", muro, new Vector3(7, -1, 0)));
        enemies.Add(new Enemy("Rose", rose, new Vector3(6.5f, -1, 0)));
        enemies.Add(new Enemy("Pb", pb, new Vector3(6.5f, -3, 0)));

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        int position = getNextEnemyPosition();
        Instantiate(enemies[position].getPrefab(), enemies[position].getPosition(), transform.rotation);

        yield return new WaitForSeconds(interval);
        StartCoroutine(Spawn());
    }

    private int getNextEnemyPosition() {
        int random = Random.Range(0, 5);
        while (enemies[random].getName() == lastEnemy) {
            random = Random.Range(0, 5);
        }
        lastEnemy = enemies[random].getName();

        return random;
    }

}
