using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 1f;
    public float maxDistance = 4f;
    public float smooth = 10f;
    Vector3 dir;
    public Vector3 dirAdjusted;
    public GameObject player;
    public float distance;
    public LayerMask ignore;

    void Start()
    {
        dir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Linecast(transform.position - new Vector3(0, 0, 10), player.transform.position, out hit, ignore))
        {
            //distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            transform.localPosition = hit.point.normalized;
        }
        else
        {
            transform.localPosition = new Vector3(0, 0.5f, -4f);
            //distance = maxDistance;
        }

        //transform.localPosition = Vector3.Lerp(transform.localPosition, dir * distance, Time.deltaTime * smooth);
    }
}
