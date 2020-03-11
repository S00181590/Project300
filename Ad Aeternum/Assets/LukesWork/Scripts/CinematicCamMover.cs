using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCamMover : MonoBehaviour
{
    public GameObject gate;
    GameObject cam, player;
    bool moving = false;

    void Start()
    {
        cam = GameObject.Find("CameraMoveController");
        player = GameObject.Find("Player");
        gate = GameObject.Find("VGate");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            moving = !moving;
        }
    }

    //void FixedUpdate()
    //{
    //    if (moving)
    //    {
    //        MoveCam();
    //        StartCoroutine(Cancel());
    //    }
    //    else
    //    {
    //        CancelInvoke();
    //    }
    //}

    //void MoveCam()
    //{
    //    cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(650, 50, 1250), Time.fixedDeltaTime);
    //    //cam.transform.LookAt(gate.transform);
    //    cam.transform.LookAt(gate.transform.position);
    //}

    public IEnumerator Cancel()
    {
        yield return new WaitForSecondsRealtime(5);

        moving = false;
    }
}
