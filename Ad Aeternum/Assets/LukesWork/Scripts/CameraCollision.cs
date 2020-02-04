using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 1f;
    public float maxDistance = 4f;
    public float smooth = 15f;
    Vector3 dir;
    public Vector3 dirAdjusted;
    public GameObject player;
    public float distance;
    public LayerMask ignore;

    private Vector2 zoomClamp = new Vector2(0, 4f);
    private float zoomDamp = 0.075f;
    private float vel = 0f;
    private float zoom;
    private float rad = 0.5f;

    Ray ray;
    Ray ray2;

    void Start()
    {
        dir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    void FixedUpdate()
    {
        ray = new Ray(player.transform.position, -transform.parent.forward);
        ray2 = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, ignore))
        {
            zoom = Mathf.SmoothDamp(zoom, hit.distance, ref vel, zoomDamp);
        }
        else
        {
            zoom = Mathf.SmoothDamp(zoom, 4, ref vel, zoomDamp);
        }

        zoom = Mathf.Clamp(zoom, zoomClamp.x, zoomClamp.y);
        transform.localPosition = new Vector3(0, 0.125f, -1) * zoom;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawRay(ray);
    //    Gizmos.DrawRay(ray2);
    //}
}
