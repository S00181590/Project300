using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    #region Variables
    Vector3 moveDir;

    StateManager states;

    CameraMoveController camManager;

    [HideInInspector]
    public bool switchLockOn = false, attackRange = false, canMove = true;

    public enum attackType { Melee, LongRange };

    private CapsuleCollider col = null;
    Transform cam;

    Animator animator;
    Rigidbody rigidBody;

    private CharacterController controller;

    private float verticalVel, jump = 400.0f, speed = 10, turnSpeed = 100, horizontal, vertical, delta;

    LayerMask groundLayers;
    #endregion

    void Start()
    {
        //states.Init();

        groundLayers = LayerMask.GetMask("Default", "Ground");

        states = GetComponent<StateManager>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;

        camManager = CameraMoveController.singleton;
    }

    void Update()
    {
        if (states.onGround)
        {
            canMove = true;
        }
        else if (!states.onGround || states.attacking || states.dodgeInput)
        {
            canMove = false;
        }

        //delta = Time.deltaTime;
        //camManager.Tick(delta);
        GetInput();
        UpdateState();
        LockOn();
    }

    void FixedUpdate()
    {
        delta = Time.fixedDeltaTime;
        
        //states.FixedTick(delta);
    }

    //private bool IsGrounded()
    //{
    //    return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 1.5f, groundLayers);
    //}

    void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
    }

    void UpdateState()
    {
        states.horizontal = horizontal;
        states.vertical = vertical;

        Vector3 v = vertical * camManager.transform.forward;
        Vector3 h = horizontal * camManager.transform.right;

        states.moveDir = (v + h).normalized;
        float m = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

        states.moveAmount = Mathf.Clamp01(m);
    }

    ////Locks on to enemy
    void LockOn()
    {
        states.horizontal = horizontal;
        states.vertical = vertical;

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Joystick1Button11))
        {
            switchLockOn = !switchLockOn;
            attackRange = !attackRange;
        }
    }

    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);

            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }

        return tMin;
    }
}
