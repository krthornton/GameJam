using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // init some vars editable in Unity
    public int startingHealth;
    public int currentHealth;
    [Tooltip("Specifies the amount of damage dealt to enemies when attacking.")]
    public int attackPoints;
    public int enemiesDefeated = 0;
    public bool dead = false;
    public AudioSource hurtSound;

    // init some private vars
    private float spawnTime;
    private List<GameObject> nearbyEnemies = new List<GameObject>();
    private Rigidbody2D rb;
    private Animator anim;

    public void Start()
    {
        // init the player's spawn time
        spawnTime = Time.time;

        // init the player's health
        currentHealth = startingHealth;

        // init rb
        rb = GetComponent<Rigidbody2D>();

        // init anim
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        // check if the player is dead
        if (!dead && currentHealth <= 0)
        {
            // call Die
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // init some vars
        GameObject enemy = collision.gameObject;
        Enemy script = enemy.GetComponent<Enemy>();

        // verify that this is an enemy
        if (enemy.CompareTag("Enemy") && !nearbyEnemies.Contains(enemy))
        {
            // add enemy to nearby list
            nearbyEnemies.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // init some vars
        GameObject enemy = collision.gameObject;

        // verfy that this is an enemy
        if (enemy.CompareTag("Enemy") && nearbyEnemies.Contains(enemy))
        {
            // remove enemy from the nearby list
            nearbyEnemies.Remove(enemy);
        }
    }

    // Function called when the player is attacking an enemy
    public void Attack()
    {
        // iterate through all the nearby enemies...
        foreach (GameObject enemy in nearbyEnemies)
        {
            // ...and damage each one
            enemy.GetComponent<Enemy>().Damaged(attackPoints);
        }
    }

    // Function called when an enemy attacks the player
    public void Damaged(int amount, Vector2 direction)
    {
        // subtract from the player's health
        currentHealth -= amount;

        // play the damaged sound
        hurtSound.Play();

        // add force to the player to knock them back
        if (direction.x > 0f)
        {
            rb.AddForce(new Vector2(-200f, 200f));
        }
        else
        {
            rb.AddForce(new Vector2(200f, 200f));
        }

        // play the hurt animation
        anim.Play("Player_Hurt");
    }

    // Function called to update the player's score
    public void UpdateScore()
    {
        // add amount to current score
        enemiesDefeated += 1;
    }

    // Function called when the player is dead
    public void Die()
    {
        // update dead
        dead = true;

        // disable the player's input
        GetComponent<CharacterController2D>().enabled = false;

        // play the hurt animation
        anim.Play("Player_Killed");

        // disable the player's colliders
        foreach (CircleCollider2D circleCollider in GetComponents<CircleCollider2D>())
        {
            circleCollider.enabled = false;
        }

        // add force to the player upwards
        rb.AddForce(new Vector2(0f, 400f));

        // save the player's time alive and enemies defeated
        PlayerPrefs.SetFloat("time_alive", Time.time - spawnTime);
        PlayerPrefs.SetInt("enemies_defeated", enemiesDefeated);

        // call GameOver
        StartCoroutine("GameOver");
    }

    // coroutine to wait and load the GameOver scene
    private IEnumerator GameOver()
    {
        // load for the death animtion to finish
        yield return new WaitForSeconds(2);

        // load GameOver scene
        SceneManager.LoadScene("GameOver");
    }
}
