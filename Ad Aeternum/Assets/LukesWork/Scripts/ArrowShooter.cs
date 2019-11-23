using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    float speed = 8f;
    public GameObject arrow;
    //public GameObject arrowSpawn;
    public GameObject spawnPoint;
    public CameraMoveController cam;
    public PlayerMoveController player;
    int count = 0;
    GameObject shootingArrow;
    Rigidbody rb;

    GameObject spawnedArrow;

    private void Update()
    {
        MakeArrow();
    }

    public void MakeArrow()
    {
        //spawnPoint = player.transform.localPosition + new Vector3(0.8f, 0, 0);

        if (cam.bowAim)
        {
            if (count <= 0)
            {
                spawnedArrow = Instantiate(arrow, spawnPoint.transform.position, Quaternion.identity);
                rb = spawnedArrow.GetComponent<Rigidbody>();

                count++;
            }

            spawnedArrow.transform.localPosition = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
            spawnedArrow.transform.localRotation = Quaternion.Euler(cam.camTransform.rotation.eulerAngles.x + 90, cam.camTransform.rotation.eulerAngles.y, cam.camTransform.localRotation.z);
            //spawnedArrow.transform.localRotation = Quaternion.Euler(0, 0, cam.transform.rotation.eulerAngles.z);
            //spawnedArrow.transform.Rotate(player.transform.position, -cam.transform.rotation.eulerAngles.z);


            if (Input.GetMouseButtonDown(0))
            {
                cam.bowAim = false;
                rb.velocity = Vector3.zero;
                rb.AddForce(spawnedArrow.transform.forward * 0.2f, ForceMode.Impulse);
                Debug.DrawLine(spawnedArrow.transform.position, spawnedArrow.transform.rotation.eulerAngles * 0.2f);
                Invoke("ShootArrow", 1);
            }
        }
    }

    void ShootArrow()
    {
        var direction = cam.camTransform.rotation.eulerAngles;
        rb.AddForce(-direction * 0.2f); 
    }
}
