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
        // Walking Forward Animation
        if (Input.GetKey(KeyCode.W)) { playerAnimation.SetBool("ForwardWalk", true); }
        else { playerAnimation.SetBool("ForwardWalk", false); }

        // Walking Backward Animation
        if (Input.GetKey(KeyCode.S)) { playerAnimation.SetBool("BackwardWalk", true); }
        else { playerAnimation.SetBool("BackwardWalk", false); }

        // Running Animation
        if (Input.GetKey(KeyCode.LeftShift)) { playerAnimation.SetBool("Run", true); }
        else { playerAnimation.SetBool("Run", false); }

        // Jump Animation
        if (Input.GetKey(KeyCode.Space)) { playerAnimation.SetBool("Jump", true); }
        else { playerAnimation.SetBool("Jump", false); }

        // Attack Animation
        if (Input.GetMouseButtonDown(0)) { playerAnimation.SetBool("Attack", true); }
        else { playerAnimation.SetBool("Attack", false); }

        // Shoot Animation
        if (Input.GetMouseButtonDown(1)) { playerAnimation.SetBool("Shoot", true); }
        else { playerAnimation.SetBool("Shoot", false); }
    }
}
