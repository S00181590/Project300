using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    #region Variables
    [HideInInspector]
    public float vertical, horizontal, rotateSpeed = 50f, speed = 5f, sprintSpeed = 1.8f, jump = 600, moveAmount = 20,
        DodgeForce = 1000f, JumpForce = 30f, moveSpeed = 2, speedModifier = 1.5f, dashTime = 2;
    float toGround = 0.76f, internalSpeedModifier, internalDashTime = 3, delta, leftTrigger, rightTrigger;

    [HideInInspector]
    public bool onGround, attacking, controllerSprint = false, dBarrier, dodgeInput, isSprinting = false, groundTest;

    [HideInInspector]
    public Vector3 moveDir, origin = Vector3.zero, origin3 = Vector3.zero, targetPos;

    //public GameObject activeModel;

    int death = 0;

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
    #endregion

    public void Init()
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
        //ignoreLayers = ~(1 << 9);
        //Cursor.visible = false;
    }

    public void Update()
    {
        onGround = OnGround();

        if (!Input.GetKeyDown(KeyCode.Space) || !Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            onGround = OnGround();
        }

        //Dodging
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button2)) && internalDashTime == 0 && onGround == true)
        {
            //internalDashTime = dashTime;
            //HandleDodging();
            //rb.velocity = moveDir * moveSpeed * internalSpeedModifier;

            //transform.position = Vector3.Lerp(player.transform.position, player.transform.position + player.transform.forward * 50, Time.deltaTime);
            //rb.AddForce(player.transform.position + player.transform.forward * 5, ForceMode.Impulse);
            //transform.position = Vector3.Slerp(gameObject.transform.position, gameObject.transform.position + (moveDir * 20) + (gameObject.transform.up * 20), Time.deltaTime * 5);
            //transform.position = Vector3.MoveTowards(transform.position, gameObject.transform.position + moveDir, 5);

            StartCoroutine(Dodging(0));
        }

        dBarrier = DeathBarrier();

        leftTrigger = Input.GetAxis("Left Trigger");
        rightTrigger = Input.GetAxis("Right Trigger");
    }

    public void FixedUpdate()
    {
        rb.isKinematic = false;

        if ((moveDir.x == 0 && moveDir.z == 0) && (Physics.Raycast(transform.position, new Vector3(0, -Vector3.up.y, 0), out hitTest, 0.81f, ignoreLayers)))
        {
            rb.isKinematic = true;
        }

        Debug.DrawRay(transform.position, new Vector3(0, -Vector3.up.y, 0) * 0.8f, Color.red);

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

            //Jump
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button1))
            {
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
        //anim.SetFloat("vertical", moveAmount, 0.4f, delta);
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

        if ((Physics.Raycast(origin, dir, out hit, dis, ignoreLayers)))
        {
            r = true;
        }

        if (moveDir.x != 0 || moveDir.z != 0)
        {
            //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            //{
            //    dis = 0;
            //}

            //if (dis != 0)
            //{

            //}
        }

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

    public bool DeathBarrier()
    {
        bool r = false;

        Vector3 origin2 = transform.position;

        Vector3 dir = -Vector3.up;
        float dis = 3;
        RaycastHit hit;
        //Debug.DrawRay(origin2, dir * dis);

        if (onGround)
        {
            origin = transform.position - moveDir; //Vector3.Lerp(origin, transform.position, 0.1f);
            death = 0;
            origin3 = Vector3.Lerp(origin3, transform.position, 0.1f);
        }

        if (Physics.Raycast(origin2, dir, out hit, dis, deathBar))
        {
            r = true;
            rb.velocity = Vector3.zero;

            targetPos = origin;
            transform.position = targetPos;

            death += 1;

            if (death == 2)
            {
                targetPos = origin3;
                transform.position = targetPos;
                death += 1;

                if (onGround)
                {
                    death = 0;
                }
            }

            if (death >= 4)
            {
                transform.position = Vector3.zero;
                death = 0;
            }
        }

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

    //void HandleDodging()
    //{
    //    if (!dodgeInput)
    //    {
    //        return;
    //    }

    //    float v = vertical;
    //    float h = horizontal;

    //    if (cam.lockOn == false)
    //    {
    //        v = 1;
    //        h = 0;
    //    }
    //    else
    //    {
    //        if (Mathf.Abs(v) < 0.3f)
    //        {
    //            v = 0;
    //        }

    //        if (Mathf.Abs(h) < 0.3f)
    //        {
    //            h = 0;
    //        }

    //        //anim.SetFloat("vertical", v);
    //        //anim.SetFloat("horizontal", h);
    //    }
    //}
}
