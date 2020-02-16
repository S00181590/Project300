using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    #region Variables
    [HideInInspector]
    public float vertical, horizontal, rotateSpeed = 50f, speed = 5f, sprintSpeed = 1.8f, jump = 600, moveAmount = 20,
        DodgeForce = 1000f, JumpForce = 30f, moveSpeed = 2, speedModifier = 1.5f, dashTime = 2;
    float toGround = 0.76f, internalSpeedModifier, internalDashTime = 3, d, leftTrigger, rightTrigger;

    [HideInInspector]
    public bool onGround, attacking, controllerSprint = false, dodgeInput, isSprinting = false, groundTest, jumpActive = false;

    [HideInInspector]
    public Vector3 moveDir;

    //public GameObject activeModel;

    Transform playerObj;

    PlayerMoveController player;
    public Camera cam;
    public CameraMoveController camMove;
    public HealthStaminaScript stamina;
    public AudioSource stepSFX;

    private Animator anim;
    private Rigidbody rb;

    public LayerMask ignoreLayers, climbableLayers, deathBar;

    RaycastHit hitTest;

    public DataHandler dataHandler;
    #endregion

    public void Start()
    {
        Application.targetFrameRate = 60;

        player = GetComponent<PlayerMoveController>();
        playerObj = GetComponent<Transform>();

        //SetupAnimator();
        rb = GetComponent<Rigidbody>();
        rb.angularDrag = 1000;
        rb.drag = 4;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        gameObject.layer = 8;

        dataHandler.OnSpawnLoad();
        transform.position = dataHandler.data.playerPosition;
    }

    public void Update()
    {
        //onGround = OnGround();

        if (!Input.GetKeyDown(KeyCode.Space) || !Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            onGround = OnGround();
        }

        if (onGround)
        {
            //Jump
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button1))
            {
                jumpActive = true;

            }
            else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button1))
            {
                jumpActive = false;
            }
        }

        //Dodging
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button2)) && internalDashTime == 0 && onGround == true)
        {
            StartCoroutine(Dodging(0));
        }

        leftTrigger = Input.GetAxis("Left Trigger");
        rightTrigger = Input.GetAxis("Right Trigger");
    }

    public void FixedUpdate()
    {
        rb.isKinematic = false;

        if ((moveDir.x == 0 && moveDir.z == 0) && /*(Physics.Raycast(transform.position, new Vector3(0, -Vector3.up.y, 0), out hitTest, 0.81f, ignoreLayers)*/ onGround == true)
        {
            rb.isKinematic = true;
        }

        if (moveAmount > 0 || onGround == false)
        {
            rb.drag = 0;
        }
        else
        {
            rb.drag = 4;
        }

        if (stamina.value > 0 && moveAmount > 0)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button10))
            {
                controllerSprint = true;
            }
        }
        else
        {
            controllerSprint = false;
        }

        if (onGround)
        {
            //Sprint
            if ((Input.GetKey(KeyCode.LeftShift) || controllerSprint == true) && stamina.canSprint)
            {
                rb.velocity = moveDir * (speed * sprintSpeed);
                isSprinting = true;
            }
            else
            {
                rb.velocity = moveDir * (speed * moveAmount);
                isSprinting = false;
            }

            if (jumpActive == true)
            {
                rb.isKinematic = false;
                rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            }

            if (internalDashTime > 0)
            {
                internalSpeedModifier = speedModifier;
                internalDashTime--;
            }

            if (internalDashTime <= 0)
            {
                internalSpeedModifier = speed;
            }

            stepSFX.UnPause();

            if (moveDir.x != 0 && moveDir.z != 0)
            {
                if (stepSFX.isPlaying == false)
                {
                    if (Input.GetKey(KeyCode.LeftShift) && stamina.value > 0)
                    {
                        if (Input.GetKey(KeyCode.Mouse1))
                        {
                            stepSFX.volume = Random.Range(0.05f, 0.1f);
                            stepSFX.pitch = Random.Range(0.3f, 0.4f);
                            stepSFX.Play();
                        }
                        else
                        {
                            stepSFX.volume = Random.Range(0.15f, 0.2f);
                            stepSFX.pitch = Random.Range(0.65f, 0.85f);
                            stepSFX.Play();
                        }
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.Mouse1))
                        {
                            stepSFX.volume = Random.Range(0.05f, 0.1f);
                            stepSFX.pitch = Random.Range(0.2f, 0.3f);
                            stepSFX.Play();
                        }
                        else
                        {
                            stepSFX.volume = Random.Range(0.1f, 0.15f);
                            stepSFX.pitch = Random.Range(0.55f, 0.75f);
                            stepSFX.Play();
                        }
                    }
                }
            }
            else
            {
                stepSFX.Pause();
            }
        }
        else
        {
            stepSFX.Pause();
            jumpActive = false;
        }

        Vector3 targetDir = moveDir;
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }

        if (!Input.GetMouseButton(1) || !Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * moveAmount * rotateSpeed);
            transform.rotation = targetRotation;
        }

        HandleMovementAnimations();
    }

    void HandleMovementAnimations()
    {
        //anim.SetFloat("vertical", moveAmount, 0.4f, d);
    }

    public bool OnGround()
    {
        bool r = false;
        Vector3 origin = transform.position;

        Vector3 dir = -Vector3.up;
        float dis = toGround;
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            dis = 0;
        }

        //if ((Physics.Raycast(origin, dir, out hit, dis, ignoreLayers)))
        //{
        //    r = true;
        //}

        //if (moveDir.x != 0 || moveDir.z != 0)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        //    {
        //        dis = 0;
        //    }
        //}

        if (Physics.Raycast(origin, new Vector3(moveDir.x * 0.2f, dir.y, moveDir.z * 0.2f), out hit, 0.8f, ignoreLayers) ||
                    Physics.Raycast(origin, new Vector3(moveDir.x * -0.3f, dir.y, moveDir.z * -0.3f), out hit, 0.9f, ignoreLayers))
        {
            r = true;
            Vector3 targetPos = hit.point;
            transform.position = new Vector3(transform.position.x, targetPos.y + 0.75f, transform.position.z);

            Debug.DrawRay(origin, new Vector3(moveDir.x * 0.2f, dir.y, moveDir.z * 0.2f) * 0.8f);
            Debug.DrawRay(origin, new Vector3(moveDir.x * -0.3f, dir.y, moveDir.z * -0.3f) * 0.9f, Color.green);
        }

        Debug.DrawRay(origin, dis * dir);


        return r;
    }

    IEnumerator Dodging(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        float startTime = Time.time;

        while (Time.time - startTime <= 0.15f)
        {
            transform.position = Vector3.Lerp(transform.position, gameObject.transform.position + moveDir * 5, Time.time - startTime);

            yield return 1;
        }
    }
}
