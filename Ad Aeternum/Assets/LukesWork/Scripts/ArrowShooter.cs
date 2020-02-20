using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowShooter : MonoBehaviour
{
    float speed = 2f;
    
    Rigidbody rb = null;
    ArrowCount count;
    
    public GameObject arrow;
    GameObject spawnPoint, spawnedArrow = null;
    Vector3 position, diff;
    Transform target;
    
    CameraCollision movecam;
    CameraMoveController cam;

    AudioSource arrowScream;

    bool active = false, moveCamBool = false;

    private void Start()
    {
        cam = GameObject.Find("CameraMoveController").GetComponent<CameraMoveController>();
        count = GameObject.Find("UICanvas").GetComponent<ArrowCount>();
        spawnPoint = GameObject.Find("ArrowSpawn");
        movecam = GameObject.Find("Main Camera").GetComponent<Camera>().gameObject.GetComponent<CameraCollision>();
        arrowScream = GameObject.Find("SFX_ArrowScream").GetComponent<AudioSource>();
        target = gameObject.transform;
    }

    private void Update()
    {
        if (count.arrowCount > 0)
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Joystick1Button6))
            {
                spawnedArrow = Instantiate(arrow, spawnPoint.transform.position, Quaternion.identity);
                active = true;
                moveCamBool = false;
                movecam.enabled = false;
                CancelInvoke();
            }
            else if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Joystick1Button6))
            {
                Destroy(spawnedArrow);
                spawnedArrow = null;
                active = false;
                moveCamBool = true;
                Invoke("MoveCam", 1);
            }

            if (spawnedArrow != null)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button7))
                {
                    count.arrowCount--;
                    ShootArrow();
                    arrowScream.Play();

                    if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Joystick1Button6))
                    {
                        SpawnArrow();
                    }
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
    }

    private void FixedUpdate()
    {
        spawnPoint = spawnPoint;
    }

    void MoveCam()
    {
        if (moveCamBool)
            movecam.enabled = true;
    }

    public void MakeArrow()
    {
        if (active)
        {
            rb = spawnedArrow.GetComponent<Rigidbody>();

            rb.isKinematic = true;
            spawnedArrow.transform.localPosition = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
            spawnedArrow.transform.localRotation = Quaternion.Euler(cam.camTransform.rotation.eulerAngles.x + 180, cam.camTransform.rotation.eulerAngles.y, cam.camTransform.localRotation.z);
        }
        else
        {
            rb = null;
        }
    }

    void SpawnArrow()
    {
        spawnedArrow = Instantiate(arrow, spawnPoint.transform.position, Quaternion.identity);
        active = true;
    }

    void ShootArrow()
    {
        active = false;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(cam.transform.forward.x * speed * 2f, cam.camTransform.forward.y + (-cam.tiltAngle * 0.05f), cam.transform.forward.z * speed * 2f, ForceMode.Impulse);
        }
    }

    void DestroyArrow()
    {
        Destroy(spawnedArrow);
    }
}
