using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    #region Variables
    public PlayerMoveController player;
    public Camera cam;

    public Transform target, camTransform,
        enemyObj, pivot;

    private float followSpeed = 10, mouseSpeed = 2, controllerSpeed = 5,
        minAngle = -30.0f, maxAngle = 50.0f, turnSmoothing = 0.1f,
        smoothX, smoothY, smoothXVel, smoothYVel,
        lookAngle, checkRadius;

    public bool lockOn = true, switchLockOn = true, bowAim = false, indicatorShowing = false;

    public float tiltAngle;

    private GameObject closestEnemy = null, intersectedEnemy = null;

    public GameObject[] enemyList;

    public GameObject hit = null, lockOnIndicator, bowAimCrosshair;

    private Vector3 distance, targetPosition, position, diff, screenPos;

    public StateManager stateManager;

    public LayerMask checkLayers;

    public Collider cylinderCol;

    Quaternion lookOnLook;
    #endregion

    private void Start()
    {
        camTransform = Camera.main.transform;

        pivot.localPosition = Vector3.zero;
        camTransform.localPosition = new Vector3(0, 0.5f, -4f);
    }

    //Essentially a Start method but accepts variables, e.g: t (the player)
    public void Init(Transform t)
    {
        target = t;
    }

    //Essentially an Update method but accepts variables, e.g: d (the deltaTime)
    public void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Mouse Input
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        //Controller Analog Stick Input
        float controllerH = Input.GetAxis("RightAxis X");
        float controllerV = Input.GetAxis("RightAxis Y");

        float targetSpeed = mouseSpeed;

        if (controllerH != 0 || controllerV != 0)
        {
            horizontal = controllerH;
            vertical = controllerV;
            targetSpeed = controllerSpeed;
        }

        if (stateManager.isSprinting)
        {
            followSpeed = 20;
        }
        else
        {
            followSpeed = 15;
        }

        //Pivot the camera on the player, muliplied by Time.deltaTime
        PivotCamOnPlayer(Time.deltaTime);

        //Handles rotations of the camera
        Rotations(Time.deltaTime, vertical, horizontal, targetSpeed);
    }

    //Makes the player the pivot for the camera
    void PivotCamOnPlayer(float d)
    {
        float speed = d * followSpeed;
        targetPosition = Vector3.Lerp(transform.position, target.position, speed / 2);
        transform.position = targetPosition;
    }

    //Handles all the rotations
    void Rotations(float d, float v, float h, float targetSpeed)
    {
        lookAngle += smoothX * targetSpeed;
        transform.rotation = Quaternion.Euler(0, lookAngle, 0);

        if (turnSmoothing > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXVel, turnSmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYVel, turnSmoothing);
        }
        else
        {
            smoothX = h;
            smoothY = v;
        }

        //If the player is locked on to an enemy
        if (player.switchLockOn)
        {
            bowAim = false;

            if ((Input.GetKey(KeyCode.Q) && !Input.GetMouseButton(1)) || (Input.GetKey(KeyCode.JoystickButton8) && !Input.GetMouseButton(1)))
            {
                closestEnemy = FindClosestEnemy();
                intersectedEnemy = IntersectedEnemy();
            }

            if (Input.GetMouseButton(1))
            {
                player.switchLockOn = false;
                bowAim = true;
            }

            //Locks on to the enemy you're looking at
            if (intersectedEnemy != null)
            {
                lookOnLook = Quaternion.LookRotation(intersectedEnemy.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(new Quaternion(0, transform.rotation.y, 0, transform.rotation.w), new Quaternion(0, lookOnLook.y, 0, lookOnLook.w), d * 15);

                tiltAngle -= smoothY * targetSpeed;
                tiltAngle = Mathf.Lerp(tiltAngle, 20, d * 15);
                pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
                lookAngle = camTransform.rotation.eulerAngles.y;

                lockOnIndicator.SetActive(true);
                screenPos = cam.WorldToScreenPoint(intersectedEnemy.transform.position);
                lockOnIndicator.transform.position = screenPos;
            }
            else if ((closestEnemy != null) && (Vector3.Distance(closestEnemy.transform.position, target.position) < 20))
            {
                lookOnLook = Quaternion.LookRotation(closestEnemy.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(new Quaternion(0, transform.rotation.y, 0, transform.rotation.w), new Quaternion(0, lookOnLook.y, 0, lookOnLook.w), d * 15);

                tiltAngle -= smoothY * targetSpeed;
                tiltAngle = Mathf.Lerp(tiltAngle, 20, d * 15);
                pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
                lookAngle = camTransform.rotation.eulerAngles.y;

                lockOnIndicator.SetActive(true);
                screenPos = cam.WorldToScreenPoint(closestEnemy.transform.position);
                lockOnIndicator.transform.position = screenPos;
            }
            else
            {
                lockOnIndicator.SetActive(false);
                player.switchLockOn = false;
            }


        }
        else
        {
            //Bow aim in
            if (Input.GetMouseButton(1))
            {
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, new Vector3(1.2f, -0.2f, -2f), 0.1f);
                camTransform.localRotation = Quaternion.Slerp(camTransform.localRotation, Quaternion.Euler(-20, 0, 0), 0.1f);

                player.transform.localRotation = Quaternion.Euler(0, camTransform.rotation.eulerAngles.y, 0);

                stateManager.speed = 2;

                bowAimCrosshair.SetActive(true);
                bowAim = true;

                lockOnIndicator.SetActive(false);

                //if (stateManager.onGround == false)
                //{
                //    Time.timeScale = 0.2f;
                //}
                //else
                //{
                //    Time.timeScale = 1f;
                //}
            }
            //Bow aim out
            else
            {
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, new Vector3(0, 0.5f, -4f), 0.1f);
                camTransform.localRotation = Quaternion.Slerp(camTransform.localRotation, Quaternion.Euler(0, 0, 0), 0.1f);

                stateManager.speed = 6;

                bowAimCrosshair.SetActive(false);
                bowAim = false;
            }

            lookAngle += smoothX * targetSpeed;
            transform.rotation = Quaternion.Euler(0, lookAngle, 0);
            tiltAngle -= smoothY * targetSpeed;
            tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
            pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);

            lockOnIndicator.SetActive(false);
        }
    }

    //Find the closest enemy to the player
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        position = target.position;

        foreach (GameObject go in gos)
        {
            diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }

    //Finds the enemy you're looking at
    public GameObject IntersectedEnemy()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        hit = null;

        foreach (GameObject eObj in enemyList)
        {
            Collider enemyCol = eObj.GetComponent<Collider>();

            if (cylinderCol.bounds.Intersects(enemyCol.bounds) || enemyCol.bounds.Intersects(cylinderCol.bounds))
            {
                hit = enemyCol.gameObject;
            }
        }

        return hit;
    }

    public static CameraMoveController singleton;

    private void Awake()
    {
        singleton = this;
    }
}