using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    GameObject player;

    Vector3 dir, dirAdjusted;
    Vector2 zoomClamp = new Vector2(0, 4f);

    public LayerMask ignore;

    public float minDistance = 0, maxDistance = 4, smooth = 10, distance = 0;
    float zoomDamp = 0.075f, vel = 0f, zoom, rad = 0.5f;

    Ray ray;

    void Start()
    {
        player = GameObject.Find("PlayerMoveController");
        ignore = LayerMask.GetMask("Default", "Ground", "Climbable");

        dir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    void FixedUpdate()
    {
        ray = new Ray(player.transform.position, -transform.parent.forward);
        RaycastHit hit;

        //if (Physics.Raycast(ray, out hit, ignore))
        //{
        //    zoom = Mathf.SmoothDamp(zoom, hit.distance, ref vel, zoomDamp);
        //}
        //else
        //{
        //    zoom = Mathf.SmoothDamp(zoom, 4, ref vel, zoomDamp);
        //}

        if (Physics.Raycast(player.transform.position, -transform.parent.forward, out hit, Mathf.Infinity, ignore))
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
}
