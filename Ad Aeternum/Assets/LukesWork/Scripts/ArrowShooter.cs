using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowShooter : MonoBehaviour
{
    public float speed = 2f;
    public GameObject arrow;
    //public GameObject arrowSpawn;
    public GameObject spawnPoint;
    public CameraMoveController cam;
    public PlayerMoveController player;
    GameObject shootingArrow = null;
    public Rigidbody rb;
    public ArrowCount count;
    public Arrow arr;

    private Vector3 position, diff;
    public Transform target;

    public GameObject spawnedArrow;
    bool active = false;
    public GameObject obj;

    private void Start()
    {
        rb = null;
    }

    private void Update()
    {
        if (count.arrowCount > 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                spawnedArrow = Instantiate(arrow, spawnPoint.transform.position, Quaternion.identity);
                active = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                Destroy(spawnedArrow);
            }

            if (Input.GetMouseButtonDown(0))
            {
                //Invoke("DestroyArrow", 1);
                count.arrowCount--;
                ShootArrow();

                if (Input.GetMouseButton(1))
                {
                    SpawnArrow();
                }
            }

            MakeArrow();
        }
        else if (count.arrowCount <= 0)
        {
            if (spawnedArrow != null)
            {
                Destroy(spawnedArrow);
            }
        }

        //obj = FindClosestArrow();

        //if (arr.collectable/* && Input.GetKey(KeyCode.F)*/)
        //{
        //    count.arrowCount++;
        //    //collectable = false;
        //    Destroy(obj);
        //}
    }

    public void MakeArrow()
    {
        if (active)
        {
            rb = spawnedArrow.GetComponent<Rigidbody>();

            rb.isKinematic = true;
            spawnedArrow.transform.localPosition = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
            spawnedArrow.transform.localRotation = Quaternion.Euler(cam.camTransform.rotation.eulerAngles.x + 180, cam.camTransform.rotation.eulerAngles.y, cam.camTransform.localRotation.z);

            //spawnedArrow.transform.localRotation = Quaternion.Euler(0, 0, cam.transform.rotation.eulerAngles.z);
            //spawnedArrow.transform.Rotate(player.transform.position, -cam.transform.rotation.eulerAngles.z);
        }
        else
        {
            rb.isKinematic = false;
        }
    }

    void SpawnArrow()
    {
        spawnedArrow = Instantiate(arrow, spawnPoint.transform.position, Quaternion.identity);
        active = true;
    }

    void ShootArrow()
    {
        //arr.trail.enabled = true;

        active = false;
        //spawnedArrow = null;

        rb.isKinematic = false;

        //rb.velocity = Vector3.zero;
        rb.AddForce(cam.transform.forward.x * speed, cam.camTransform.forward.y + 0.75f + (-cam.tiltAngle * 0.01f), cam.transform.forward.z * speed, ForceMode.Impulse);        
    }

    void DestroyArrow()
    {
        Destroy(spawnedArrow);
    }
}
