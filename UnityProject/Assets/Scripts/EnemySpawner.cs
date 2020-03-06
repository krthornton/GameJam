using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // init some vars editable in Unity
    public GameObject enemyPrefab;

    // Function used to spawn a new enemy
    public GameObject Spawn()
    {
        // instantiate a new enemy from the prefab at location of spawner
        return Instantiate(enemyPrefab, transform);
    }
}
