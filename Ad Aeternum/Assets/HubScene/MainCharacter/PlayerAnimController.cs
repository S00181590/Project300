using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator anim;
    StateManager state;

    void Start()
    {
        anim = GameObject.Find("Player").GetComponent<Animator>();
        state = GameObject.Find("PlayerMoveController").GetComponent<StateManager>();
    }

    void Update()
    {
        if ((state.moveDir.x != 0 && state.moveDir.z != 0) && state.onGround)
        {
            if (state.isSprinting)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isWalking", false);
            }
            else
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }
    }
}
