using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    // init some public vars (editable in Unity)
    public float speed;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // on startup, set target to the player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update called on a fixed time interval
    private void FixedUpdate()
    {
        // chase after the player
        if (Vector2.Distance(transform.position, target.position) > 2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        }
    }
}
