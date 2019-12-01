using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public Vector3 moveDir;
    private float horizontal;
    private float vertical;

    private float delta;

    StateManager states;

    CameraMoveController camManager;

    public bool switchLockOn = false;

    public bool attackRange = false;
    public enum attackRangeType { shortRange, longRange };

    private CapsuleCollider col = null;
    Transform cam;

    public float speed;
    public float turnSpeed = 100;

    public GameObject enemy;

    Animator animator;
    Rigidbody rigidBody;

    private CharacterController controller;

    private float verticalVel;
    public float jump = 10.0f;

    public LayerMask groundLayers;

    public bool canMove = true;
    public StateManager stateManager;

    void Start()
    {
        states = GetComponent<StateManager>();
        states.Init();

        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        camManager = CameraMoveController.singleton;

        camManager.Init(this.transform);

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (stateManager.onGround)
        {
            canMove = true;
        }
        else if (!stateManager.onGround || stateManager.attacking || stateManager.dodgeInput)
        {
            canMove = false;
        }

        delta = Time.deltaTime;
        states.Tick(delta);
        LockOn();
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 1.5f, groundLayers);
    }

    void FixedUpdate()
    {
        delta = Time.fixedDeltaTime;
        camManager.Tick(delta);
        GetInput();
        UpdateState();
        states.FixedTick(delta);
    }

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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            switchLockOn = !switchLockOn;
            attackRange = !attackRange;
        }

        camManager.Init(this.transform);
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
