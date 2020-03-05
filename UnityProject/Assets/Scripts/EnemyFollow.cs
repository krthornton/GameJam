using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    // init some public vars (editable in Unity)
    public float speed;
    public Transform target;

    // init some private variables
    Rigidbody2D rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // on startup, set target to the player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // on startup, get rigidbody component
        rb = gameObject.GetComponent<Rigidbody2D>();

        // on startup, get animator component
        anim = gameObject.GetComponent<Animator>();
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
            // check which way the enemy is moving and flip sprite if necessary
            if ((transform.position.x - target.position.x) > 0f)
            {
                // flip the enemy sprite to face left
                Vector3 theScale = transform.localScale;
                theScale.x = -Mathf.Abs(theScale.x);
                transform.localScale = theScale;
            }
            else
            {
                // flip the enemy sprite to face right
                Vector3 theScale = transform.localScale;
                theScale.x = Mathf.Abs(theScale.x);
                transform.localScale = theScale;
            }

            // play the running animation
            anim.SetBool("isRunning", true);

            // move the enemy towards the player
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        }
        else
        {
            // stop the running animation
            anim.SetBool("isRunning", false);
        }

    }
}
