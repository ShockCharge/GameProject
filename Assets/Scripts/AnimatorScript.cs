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
        if (Input.GetKey(KeyCode.W))
        {
            playerAnimation.SetBool("ForwardWalk", true);
        }
        else
        {
            playerAnimation.SetBool("ForwardWalk", false);
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

        // Attack Animation (independent of movement)
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimation.SetBool("Attack", true);
        }
        else
        {
            playerAnimation.SetBool("Attack", false);
        }

        // Shoot Animation (independent of movement)
        if (Input.GetMouseButtonDown(1))
        {
            playerAnimation.SetBool("Shoot", true);
        }
        else
        {
            playerAnimation.SetBool("Shoot", false);
        }
    }
}
