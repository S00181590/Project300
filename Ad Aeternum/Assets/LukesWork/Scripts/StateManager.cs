using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public Vector3 moveDir;
    public float rotateSpeed = 5f;

    public float speed = 5f;
    public float sprintSpeed = 1.5f;
    public float jump = 500f;

    public GameObject activeModel;
    private Animator anim;
    private Rigidbody rb;

    public float moveAmount;

    private float delta;

    public LayerMask ignoreLayers;
    public LayerMask deathBar;
    public bool onGround;
    public bool dBarrier;
    public float toGround = 0.75f;

    Vector3 origin = Vector3.zero;
    Vector3 origin3 = Vector3.zero;
    Vector3 targetPos;
    public int death = 0;

    public PlayerMoveController player;
    public Transform playerObj;

    private CameraMoveController cam;
    public bool dodgeInput;
    public float DodgeForce = 500f;
    public float JumpForce = 30f;

    bool groundTest;
    RaycastHit hitTest;

    int count = 0;

    public void Init()
    {
        //SetupAnimator();
        rb = GetComponent<Rigidbody>();
        rb.angularDrag = 1000;
        rb.drag = 4;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        gameObject.layer = 8;
        //ignoreLayers = ~(1 << 9);
        Cursor.visible = false;
    }

    public void Tick(float d)
    {
        delta = d;
        onGround = OnGround();

        if (!Input.GetKeyDown(KeyCode.Space))
        {
            onGround = OnGround();
        }

        dBarrier = DeathBarrier();
    }

    public void FixedTick(float d)
    {
        delta = d;

        if (Input.GetMouseButtonDown(1) && count <= 0)
        {
            count = 0;
            count++;
            speed = 2;
        }

        if (Input.GetMouseButtonUp(1) && count >= 1)
        {
            count = 1;
            count--;
            speed = 6;
        }

        rb.isKinematic = false;

        if ((moveDir.x == 0 && moveDir.z == 0) && (Physics.Raycast(transform.position, new Vector3(0, -Vector3.up.y, 0), out hitTest, 0.8f, ignoreLayers)))
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

        if (onGround)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = moveDir * (speed * sprintSpeed);

                //if (Input.GetKey(KeyCode.Space))
                //{
                //    rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
                //}
            }
            else
            {
                rb.velocity = moveDir * (speed * moveAmount);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                //HandleDodging();
                //Vector3 dash = Vector3.Lerp(playerObj.position, Vector3.forward, 10f);
                //Vector3 dash = new Vector3(playerObj.position, move, 10f);
                //rb.AddForce(moveDir.x * (speed * d), 0, moveDir.z * (speed * d));
                //rb.AddForce(horizontal * 2, 0, vertical * 2);
                //rb.velocity = moveDir * (speed * (d * 200));

                rb.drag = 0f;

                rb.AddForce(new Vector3(0f, JumpForce / 2, 0f), ForceMode.Impulse);
                rb.AddForce(new Vector3(moveDir.x * DodgeForce, moveDir.y * DodgeForce, moveDir.z * DodgeForce), ForceMode.Impulse);
            }
        }

        Vector3 targetDir = moveDir;
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, delta * moveAmount * rotateSpeed);
        transform.rotation = targetRotation;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dis = 0;
        }

        if ((Physics.Raycast(origin, dir, out hit, dis, ignoreLayers)))
        {
            r = true;
        }

        if (moveDir.x != 0 || moveDir.z != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dis = 0;
            }

            if (dis != 0)
                if (Physics.Raycast(origin, new Vector3(moveDir.x * 0.2f, dir.y, moveDir.z * 0.2f), out hit, 0.8f, ignoreLayers) ||
                    Physics.Raycast(origin, new Vector3(moveDir.x * -0.3f, dir.y, moveDir.z * -0.3f), out hit, 0.9f, ignoreLayers))
                {
                    r = true;
                    Vector3 targetPos = hit.point;
                    transform.position = new Vector3(transform.position.x, targetPos.y + 0.75f, transform.position.z);
                }
        }


        Debug.DrawRay(origin, dis * dir);
        Debug.DrawRay(origin, new Vector3(moveDir.x * 0.2f, dir.y, moveDir.z * 0.2f) * 0.8f);
        Debug.DrawRay(origin, new Vector3(moveDir.x * -0.3f, dir.y, moveDir.z * -0.3f) * 0.9f);

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

    void HandleDodging()
    {
        if (!dodgeInput)
        {
            return;
        }

        float v = vertical;
        float h = horizontal;

        if (cam.lockOn == false)
        {
            v = 1;
            h = 0;
        }
        else
        {
            if (Mathf.Abs(v) < 0.3f)
            {
                v = 0;
            }

            if (Mathf.Abs(h) < 0.3f)
            {
                h = 0;
            }

            //anim.SetFloat("vertical", v);
            //anim.SetFloat("horizontal", h);
        }
    }
}
