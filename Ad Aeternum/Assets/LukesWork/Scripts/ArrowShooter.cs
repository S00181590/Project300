using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : Arrow
{
    float speed = 8f;
    public GameObject arrow;
    //public GameObject arrowSpawn;
    Vector3 spawnPoint;
    public CameraMoveController cam;
    public PlayerMoveController player;
    int count = 0;
    GameObject shootingArrow;
    Rigidbody rb;

    public override void MakeArrow()
    {
        spawnPoint = player.transform.localPosition + new Vector3(0.8f, 0, 0);

        if (cam.bowAim)
        {
            if (count <= 0)
            {
                var spawnedArrow = Instantiate(arrow, spawnPoint, Quaternion.identity);
                rb = spawnedArrow.GetComponent<Rigidbody>();

                count++;
            }

            arrow.transform.position = spawnPoint;
            arrow.transform.rotation = Quaternion.Euler(90, 0, 0);


            if (Input.GetMouseButton(0))
            {
                Invoke("ShootArrow", 1);
            }

            base.MakeArrow();
        }
    }

    void ShootArrow()
    {
        var direction = cam.camTransform.rotation.eulerAngles;
        rb.AddForce(direction * speed);
    }
}
