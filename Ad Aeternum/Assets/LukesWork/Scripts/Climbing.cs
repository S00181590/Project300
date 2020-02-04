using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    PlayerMoveController player;
    StateManager state;

    bool canClimb = false;

    public LayerMask climbableLayers;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = this.gameObject.GetComponent<PlayerMoveController>();
        state = this.gameObject.GetComponent<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Climb();
    }

    public void Climb()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(
            player.transform.position.x,
            player.transform.position.y - 0.4f,
            player.transform.position.z),
            new Vector3(state.moveDir.x, state.moveDir.y + 0.2f, state.moveDir.z), 
            out hit, 0.8f, climbableLayers))
        {
            canClimb = true;
            Invoke("JumpClimb", 1);
            
        }
        else
        {
            canClimb = false;
            //CancelInvoke();
        }

        Debug.DrawRay(new Vector3(player.transform.position.x, player.transform.position.y - 0.4f, player.transform.position.z), 
            new Vector3(state.moveDir.x, state.moveDir.y + 0.2f, state.moveDir.z) * 0.8f, Color.yellow);
    }

    public void JumpClimb()
    {
        Invoke("WhileClimbing", 0.5f);
        rb.AddForce(Vector3.up * 600, ForceMode.Impulse);
        if (state.onGround && canClimb)
        {
            
            rb.AddForce(state.moveDir * 200, ForceMode.Impulse);
        }
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
                rb.AddForce(Vector3.up * 40, ForceMode.Impulse);

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
