using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    PlayerMoveController player;
    StateManager state;
    bool canClimb = false;
    LayerMask climbableLayers;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerMoveController>();
        state = GetComponent<StateManager>();
        climbableLayers = LayerMask.GetMask("Climbable");
    }
    
    void Update()
    {
        Climb();
    }

    void Climb()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(player.transform.position.x, player.transform.position.y - 0.4f, player.transform.position.z), 
            new Vector3(state.moveDir.x, state.moveDir.y/* + 0.2f*/, state.moveDir.z), 
            out hit, 1, climbableLayers))
        {
            Invoke("JumpClimb", 0.5f);
            Invoke("WhileClimbing", 1);
            canClimb = true;
        }
        else
        {
            canClimb = false;
            //CancelInvoke();
        }
    }

    public void JumpClimb()
    {
        if (state.onGround && canClimb)
        {
            state.jumpActive = true;
            rb.isKinematic = false;
            rb.AddForce(Vector3.up * 600, ForceMode.Impulse);
            rb.AddForce(state.moveDir * 200, ForceMode.Impulse);
            //canClimb = false;
        }

        Debug.DrawRay(new Vector3(player.transform.position.x, player.transform.position.y - 0.4f, player.transform.position.z),
            new Vector3(state.moveDir.x, state.moveDir.y + 0.2f, state.moveDir.z) * 1, Color.yellow);
    }

    public void WhileClimbing()
    {
        if (canClimb == true)
        {
            rb.velocity = Vector3.up * state.moveSpeed;
            //rb.velocity = state.moveDir * 5;
        }

        RaycastHit hit;

        if (Physics.Raycast(new Vector3(
            player.transform.position.x,
            player.transform.position.y - 0.75f,
            player.transform.position.z), state.moveDir,
            out hit, 0.8f, climbableLayers)
            &&
            !Physics.Raycast(new Vector3(
            player.transform.position.x,
            player.transform.position.y - 0.4f,
            player.transform.position.z),
            new Vector3(state.moveDir.x, state.moveDir.y + 0.2f, state.moveDir.z),
            out hit, 0.8f, climbableLayers))
        {
            if (!state.onGround)
            {
                rb.AddForce(state.moveDir * 20, ForceMode.Impulse);
                rb.AddForce(Vector3.up * 50, ForceMode.Impulse);

                player.canMove = false;
            }
            else
            {
                JumpClimb();
            }
        }

        Debug.DrawRay(new Vector3(player.transform.position.x, player.transform.position.y - 0.75f, player.transform.position.z), state.moveDir * 0.8f);
        //Debug.DrawRay(new Vector3(player.transform.position.x, player.transform.position.y - 1f * 0.2f, player.transform.position.z), state.moveDir * 0.8f);
        //Debug.DrawRay(new Vector3(player.transform.position.x, player.transform.position.y - 1f * -0.2f, player.transform.position.z), state.moveDir * 0.8f);

    }
}
