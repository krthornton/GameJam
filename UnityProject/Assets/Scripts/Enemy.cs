using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // init some public vars (editable in Unity)
    public float speed;
    public int startingHealth;
    public float attackDelay;
    [Tooltip("Specifies the amount of damage dealt to the player when attacking.")]
    public int attackPoints;
    [Tooltip("Specifies the minimum attack distance between the enemy and the player.")]
    public float attackDistance;

    // init some private variables
    private Rigidbody2D rb;
    private Animator anim;
    private int currentHealth;
    private bool dead;
    private float detectionTime;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // on startup, set target to the player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // on startup, get rigidbody component
        rb = gameObject.GetComponent<Rigidbody2D>();

        // on startup, get animator component
        anim = gameObject.GetComponent<Animator>();

        // setup health
        currentHealth = startingHealth;
    }

    // Update called on a fixed time interval
    private void FixedUpdate()
    {
        // check if this enemy should be dead
        if (!dead && currentHealth <= 0) Die();

        // chase after the player
        if (Vector2.Distance(transform.position, target.position) > attackDistance)
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

            // check to attack the player
            if (detectionTime != 0f && Time.time - detectionTime >= attackDelay)
            {
                // play the attack animation
                anim.Play("Enemy_Attack");

                // call damaged on the player to deal damage
                target.gameObject.GetComponent<Player>().Damaged(
                    attackPoints,
                    transform.position
                );

                // reset the detectionTime
                detectionTime = Time.time;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // init some vars
        GameObject player = collision.gameObject;

        // check if this is the player
        if (player.CompareTag("Player"))
        {
            // set the detection time
            detectionTime = Time.time;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // init some vars
        GameObject player = collision.gameObject;

        // check if this is the player
        if (player.CompareTag("Player"))
        {
            // set the detection time
            detectionTime = 0f;
        }
    }

    public void Damaged(int damageTaken)
    {
        // add force to the enemy, knocking them back
        if (transform.localScale.x > 0f)
        {
            rb.AddForce(new Vector2(-2000f, 2000f));
        }
        else
        {
            rb.AddForce(new Vector2(2000f, 2000f));
        }
        
        // subtract health from enemy
        currentHealth -= damageTaken;

        // play the hurt animation
        anim.Play("Enemy_Hurt");
    }

    public void Die()
    {
        // set dead
        dead = true;

        // disable the colliders so the enemy falls off the screen
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        // destroy self after set time
        Destroy(gameObject, 1.0f);

        // increment the player's score
        target.gameObject.GetComponent<Player>().UpdateScore();
    }
}
