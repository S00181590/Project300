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

    private CapsuleCollider col;
    Transform cam;

    public float speed = 10f;
    public float turnSpeed = 100;

    public GameObject enemy;

    Animator animator;
    Rigidbody rigidBody;

    private CharacterController controller;

    private float verticalVel;
    private float gravity = 14.0f;
    public float jump = 10.0f;

    public LayerMask groundLayers;

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
        //if (controller.isGrounded)
        //{
        //    verticalVel = -gravity * Time.deltaTime;
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        verticalVel = jump;
        //    }
        //}
        //else
        //{
        //    verticalVel -= gravity * Time.deltaTime;
        //}

        //Vector3 movevector = new Vector3(0, verticalVel, 0);

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
        //right is shorthand for (1,0,0) or the x value            forward is short for (0,0,1) or the z value 
        //Vector3 dir = (cam.right * Input.GetAxis("Horizontal")) + (cam.forward * Input.GetAxis("Vertical"));

        //dir.y = 0;//Keeps character upright against slight fluctuations

        //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        //{
        //    //rotate from this /........to this............../.........at this speed 
        //    rigidBody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);

        //    if (Input.GetKey(KeyCode.LeftShift))
        //    {
        //        //rigidBody.velocity = transform.forward * speed * 2;
        //        //rigidBody.transform.position = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime, 0);
        //        GetInput();
        //    }
        //    else
        //    {
        //        //rigidBody.velocity = transform.forward * speed;
        //        GetInput();
        //    }

        //    animator.SetInteger("animation", 10);//Walk or run animation works well here
        //}
        //else
        //{
        //    animator.SetInteger("animation", 25);//Idle animation works well here
        //}

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

    void LockOn()
    {
        states.horizontal = horizontal;
        states.vertical = vertical;

        if (Input.GetKeyDown(KeyCode.Q) || (Input.GetMouseButtonDown(1)))
        {
            switchLockOn = !switchLockOn;
            attackRange = !attackRange;
        }

        //if (switchLockOn)
        //{
        camManager.Init(this.transform);
        //}
        //else
        //{
        //    camManager.Init(this.transform);
        //}
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
