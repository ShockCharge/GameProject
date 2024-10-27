using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    Animator playerAnimation;
    ControllerScript playerControl;

    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        playerControl = GetComponent<ControllerScript>();
    }

    void Update()
    {
        // Walking Animation
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            playerAnimation.SetBool("Walk", true);
        }
        else
        {
            playerAnimation.SetBool("Walk", false);
        }

        // Running Animation
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            playerAnimation.SetBool("Run", true);
        }
        else
        {
            playerAnimation.SetBool("Run", false);
        }

        // Jump Animation
        if (playerControl.velocity.y > 0) // Jumping
        {
            playerAnimation.SetBool("Jump", true);
        }
        else if (playerControl.velocity.y < 0) // Falling
        {
            playerAnimation.SetBool("Jump", false);
        }
        else if (playerControl.velocity.y == 0) // On the ground
        {
            playerAnimation.SetBool("Jump", false);
        }

        // Attack Animation (independent of movement)
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimation.SetBool("Attack", true);
        }
        else
        {
            playerAnimation.SetBool("Attack", false);
        }
    }
}
