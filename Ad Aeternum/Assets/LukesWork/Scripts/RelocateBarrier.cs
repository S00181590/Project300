using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelocateBarrier : MonoBehaviour
{
    StateManager state;
    Rigidbody rb;
    int relocations = 0;
    public LayerMask barriers;
    Vector3 origin = Vector3.zero, origin3 = Vector3.zero, targetPos;
    bool barrierBool;

    void Start()
    {
        state = GetComponent<StateManager>();
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        barrierBool = Relocate();
    }

    public bool Relocate()
    {
        bool r = false;

        Vector3 origin2 = transform.position;

        Vector3 dir = -Vector3.up;
        float dis = 3;
        RaycastHit hit;

        if (state.onGround)
        {
            origin = transform.position - state.moveDir;
            relocations = 0;
            origin3 = Vector3.Lerp(origin3, transform.position, 0.1f);
        }

        if (Physics.Raycast(origin2, dir, out hit, dis, barriers))
        {
            r = true;
            rb.velocity = Vector3.zero;

            targetPos = origin;
            transform.position = targetPos;

            relocations += 1;

            if (relocations == 2)
            {
                targetPos = origin3;
                transform.position = targetPos;
                relocations += 1;

                if (state.onGround)
                {
                    relocations = 0;
                }
            }

            if (relocations >= 4)
            {
                transform.position = Vector3.zero;
                relocations = 0;
            }
        }

        return r;
    }
}
