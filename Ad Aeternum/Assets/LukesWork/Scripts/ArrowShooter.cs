using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowShooter : MonoBehaviour
{
    float speed = 2f;
    public GameObject arrow;
    //public GameObject arrowSpawn;
    public GameObject spawnPoint;
    public CameraMoveController cam;
    public PlayerMoveController player;
    int count = 0;
    GameObject shootingArrow = null;
    public Rigidbody rb;

    public GameObject spawnedArrow;
    bool active = false;

    private void Start()
    {
        rb = null;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            spawnedArrow = Instantiate(arrow, spawnPoint.transform.position, Quaternion.identity);
            active = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            //Destroy(spawnedArrow);
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            //Invoke("DestroyArrow", 1);
            Invoke("ShootArrow", 0.5f);

            if (Input.GetMouseButton(1))
            {
                Invoke("SpawnArrow", 0.5f);
                
            }
        }

        MakeArrow();
    }

    public void MakeArrow()
    {
        //spawnPoint = player.transform.localPosition + new Vector3(0.8f, 0, 0);

        if (active)
        {
            //if (count <= 0)
            //{

                rb = spawnedArrow.GetComponent<Rigidbody>();

            //    count++;
            //}

            rb.isKinematic = true;
            spawnedArrow.transform.localPosition = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
            spawnedArrow.transform.localRotation = Quaternion.Euler(cam.camTransform.rotation.eulerAngles.x + 90, cam.camTransform.rotation.eulerAngles.y, cam.camTransform.localRotation.z);
            //spawnedArrow.transform.localRotation = Quaternion.Euler(0, 0, cam.transform.rotation.eulerAngles.z);
            //spawnedArrow.transform.Rotate(player.transform.position, -cam.transform.rotation.eulerAngles.z);


            if (Input.GetMouseButtonDown(0))
            {
                //active = true;
                //cam.bowAim = false;




                //Invoke("ShootArrow", 1);
            }
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
        //spawnedArrow = null;

        rb.isKinematic = false;

        //rb.velocity = Vector3.zero;
        rb.AddForce(cam.transform.forward.x * speed, cam.camTransform.forward.y + 0.75f + (-cam.tiltAngle * 0.01f), cam.transform.forward.z * speed, ForceMode.Impulse);
        active = false;
        
    }

    void DestroyArrow()
    {
        Destroy(spawnedArrow);
    }
}
