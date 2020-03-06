using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    // init some vars editable in Unity
    public float spawnDelay;

    // init some private vars
    private float lastSpawnTime;
    private GameObject[] spawnpoints;
    private List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // init the last spawn time
        lastSpawnTime = Time.time;

        // init the list of spawnpoints upon startup
        spawnpoints = GameObject.FindGameObjectsWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        // check if it's been longer than spawnDelay
        if (Time.time - lastSpawnTime >= spawnDelay)
        {
            // pick a random spawn point and call spawn on that point
            enemies.Add(
                spawnpoints[Random.Range(0, spawnpoints.Length)]
                    .GetComponent<EnemySpawner>()
                    .Spawn()
            );

            // reset last spawn time
            lastSpawnTime = Time.time;
        }
    }
}
