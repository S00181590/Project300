using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public ArrowCount count;
    GameObject closestArrow;

    private Vector3 position, diff;
    public Transform target;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F) && other.tag == "Arrow")
        {
            closestArrow = FindClosestArrow();

            Destroy(closestArrow);

            count.arrowCount++;
        }
    }

    public GameObject FindClosestArrow()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Arrow");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        position = target.position;

        foreach (GameObject go in gos)
        {
            diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }
}
