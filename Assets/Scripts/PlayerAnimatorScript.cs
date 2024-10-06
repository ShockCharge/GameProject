using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    Animator Player;

    void Start()
    {
        Player = GetComponent<Animator>();
    }

    void Update()
    {
        // Walking Forward Animation
        if (Input.GetKey(KeyCode.W)) { Player.SetBool("ForwardWalk", true); }
        else { Player.SetBool("ForwardWalk", false); }

        // Walking Backward Animation
        if (Input.GetKey(KeyCode.S)) { Player.SetBool("BackwardWalk", true); }
        else { Player.SetBool("BackwardWalk", false); }

        // Running Animation
        if (Input.GetKey(KeyCode.LeftShift)) { Player.SetBool("Run", true); }
        else { Player.SetBool("Run", false); }

        // Jump Animation
        if (Input.GetKey(KeyCode.Space)) { Player.SetBool("Jump", true); }
        else { Player.SetBool("Jump", false); }

        // Attack Animation
        if (Input.GetMouseButtonDown(0)) { Player.SetBool("Attack", true); }
        else { Player.SetBool("Attack", false); }
    }
}
