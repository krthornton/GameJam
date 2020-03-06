using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // init some public vars (editabl in Unity)
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;

    // init some private vars
    private float horizontalMove = 0f;
    private bool jump = false;
    private Player playerComponent;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // get the Player script component on startup
        playerComponent = gameObject.GetComponent<Player>();

        // get the Animator component
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the user is moving
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (horizontalMove != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // Check if the user is trying to attack
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        // Check if the user is trying to jump
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJump", true);
        }
        else
        {
            animator.SetBool("isJump", false);
        }
    }

    private void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    private void Attack()
    {
        // Check if the player is already attacking
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))
        {
            return;
        }

        // play the attack animation
        animator.Play("Player_Attack");

        // call attack on playerComponent
        playerComponent.Attack();
    }
}
