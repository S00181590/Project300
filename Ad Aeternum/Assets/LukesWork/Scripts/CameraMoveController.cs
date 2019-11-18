using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    #region Variables
    public bool lockOn = true, switchLockOn = true;

    public Transform target, camTransform,
        playerObj, playerLocation, enemyObj,
        pivot, pivotPos;

    public float followSpeed = 15, mouseSpeed = 1, controllerSpeed = 5,
        minAngle = -15.0f, maxAngle = 50.0f, turnSmoothing = 0.1f,
        smoothX, smoothY, smoothXVel, smoothYVel,
        lookAngle, tiltAngle,
        checkRadius;

    public GameObject closestEnemy = null, intersectedEnemy = null;

    public Vector3 distance, targetPosition, position, diff;

    public PlayerMoveController player;

    public StateManager stateManager;

    public LayerMask checkLayers;

    public Collider cylinderCol;

    Quaternion lookOnLook;

    public GameObject[] enemyList;

    public GameObject hit = null;

    private int count = 0;
    private bool bowAim = false;
    #endregion

    //Essentially a Start method but accepts variables
    public void Init(Transform t)
    {
        target = t;
        camTransform = Camera.main.transform;
        pivot = pivotPos;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Tick(float d)
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

        //Pivot the camera on the player, muliplied by Time.deltaTime
        PivotCamOnPlayer(d);

        //Handles rotations of the camera
        Rotations(d, vertical, horizontal, targetSpeed);
    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        if (!player.switchLockOn)
        {
            
        }
    }

    void PivotCamOnPlayer(float d)
    {
        float speed = d * followSpeed;
        targetPosition = Vector3.Lerp(transform.position, target.position, speed / 2);
        transform.position = targetPosition;
    }

    void Rotations(float d, float v, float h, float targetSpeed)
    {
        //Finds the closest enemy to the player


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

        if (player.switchLockOn)
        {
            if (Input.GetKey(KeyCode.Q)) //|| (Input.GetMouseButton(1)))
            {
                closestEnemy = FindClosestEnemy();
                intersectedEnemy = IntersectedEnemy();

            }
            //else
            //{
            //    closestEnemy = null;
            //}



            //if ((closestEnemy != null) && (intersectedEnemy != null) && (Vector3.Distance(closestEnemy.transform.position, playerObj.position) < 20))
            //{
            //    lookOnLook = Quaternion.LookRotation(intersectedEnemy.transform.position - transform.position);
            //    transform.rotation = Quaternion.Slerp(new Quaternion(0, transform.rotation.y, 0, transform.rotation.w), new Quaternion(0, lookOnLook.y, 0, lookOnLook.w), d * 5);

            //    tiltAngle -= smoothY * targetSpeed;
            //    tiltAngle = Mathf.Lerp(tiltAngle, 20, d * 5);
            //    pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);

            //    lookAngle = camTransform.rotation.eulerAngles.y;
            //}
            //else 


            if (intersectedEnemy != null)
            {
                lookOnLook = Quaternion.LookRotation(intersectedEnemy.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(new Quaternion(0, transform.rotation.y, 0, transform.rotation.w), new Quaternion(0, lookOnLook.y, 0, lookOnLook.w), d * 15);

                tiltAngle -= smoothY * targetSpeed;
                tiltAngle = Mathf.Lerp(tiltAngle, 20, d * 15);
                pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);

                lookAngle = camTransform.rotation.eulerAngles.y;
            }
            else if ((closestEnemy != null) && (Vector3.Distance(closestEnemy.transform.position, playerObj.position) < 20))
            {

                //LockOnVector = new Vector3(closestEnemy.transform.position.x, playerObj.position.y, closestEnemy.transform.position.z);
                //Vector3 aaa = new Vector3(camTransform.rotation.x, camTransform.rotation.y, 10);
                //LockOnLerp = Vector3.Lerp(LockOnVector, aaa, 2 * d);


                //else
                //{
                lookOnLook = Quaternion.LookRotation(closestEnemy.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(new Quaternion(0, transform.rotation.y, 0, transform.rotation.w), new Quaternion(0, lookOnLook.y, 0, lookOnLook.w), d * 15);

                tiltAngle -= smoothY * targetSpeed;
                tiltAngle = Mathf.Lerp(tiltAngle, 20, d * 15);
                pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
                //transform.LookAt(LockOnVector);
                lookAngle = camTransform.rotation.eulerAngles.y;
                //}
            }
            else
            {
                player.switchLockOn = false;
            }
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                Vector3 v1 = new Vector3(1.2f, 0.25f, -2f);

                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, new Vector3(1.2f, 0.25f, -2f), 0.1f);
                player.transform.localRotation = Quaternion.Euler(0, camTransform.rotation.eulerAngles.y, 0);
                //pivot.localRotation = Quaternion.Euler(-20, 0, 0);
                stateManager.speed = 3;

                //if (stateManager.onGround == false)
                //{
                //    Time.timeScale = 2f;
                //}
                //else
                //{
                //    Time.timeScale = 1f;
                //}
            }
            else
            {
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, new Vector3(0, 0.5f, -4), 0.1f);
                //pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
                stateManager.speed = 6;
            }

            lookAngle += smoothX * targetSpeed;
            transform.rotation = Quaternion.Euler(0, lookAngle, 0);

            tiltAngle -= smoothY * targetSpeed;
            tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
            pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
        }
    }

    void BowAim()
    {
        if (Input.GetMouseButtonDown(1) && count <= 0)
        {
            count = 0;
            count++;
            camTransform.localPosition = new Vector3(camTransform.localPosition.x + 0.5f, camTransform.localPosition.y - 0.2f, camTransform.localPosition.z + 1.5f);
        }

        if (Input.GetMouseButtonUp(1) && count >= 1)
        {
            count = 1;
            count--;
            camTransform.localPosition = new Vector3(camTransform.localPosition.x - 0.5f, camTransform.localPosition.y + 0.2f, camTransform.localPosition.z - 1.5f);
        }
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        position = playerObj.position;

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

    public GameObject IntersectedEnemy()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        hit = null;

        foreach (GameObject eObj in enemyList)
        {
            Collider enemyCol = eObj.GetComponent<Collider>();

            if (cylinderCol.bounds.Intersects(enemyCol.bounds) || enemyCol.bounds.Intersects(cylinderCol.bounds))
            {
                //float distance = Mathf.Infinity;
                //position = playerObj.position;

                //diff = enemyCol.transform.position - position;
                //float curDistance = diff.sqrMagnitude;

                //if (curDistance < distance)
                //{
                //hit = enemyCol.GetComponent<GameObject>();
                hit = enemyCol.gameObject;
                //    distance = curDistance;
                //}
            }
            //else
            //{
            //    hit = null;
            //}
            //else
            //{
            //    enemy = hit;
            //}
        }

        return hit;
    }

    public static CameraMoveController singleton;

    private void Awake()
    {
        singleton = this;
    }
}
