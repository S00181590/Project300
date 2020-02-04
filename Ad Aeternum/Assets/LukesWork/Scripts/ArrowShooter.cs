using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowShooter : MonoBehaviour
{
    public float speed = 2f;
    public GameObject arrow;
    public GameObject spawnPoint;
    public CameraMoveController cam;
    public PlayerMoveController player;
    public Rigidbody rb = null;
    public ArrowCount count;
    public Arrow arr;

    private Vector3 position, diff;
    public Transform target;

    public GameObject spawnedArrow = null;
    bool active = false;
    public GameObject obj = null;

    public Camera camera;
    public CameraCollision movecam;
    bool moveCamBool = false;

    private void Start()
    {
        movecam = camera.gameObject.GetComponent<CameraCollision>();
    }

    private void Update()
    {
        if (count.arrowCount > 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                spawnedArrow = Instantiate(arrow, spawnPoint.transform.position, Quaternion.identity);
                active = true;
                moveCamBool = false;
                movecam.enabled = false;
                CancelInvoke();
            }
            else if (Input.GetMouseButtonUp(1))
            {
                Destroy(spawnedArrow);
                spawnedArrow = null;
                active = false;
                moveCamBool = true;
                Invoke("MoveCam", 1);
            }

            if (spawnedArrow != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    count.arrowCount--;
                    ShootArrow();

                    if (Input.GetMouseButton(1))
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
