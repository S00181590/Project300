using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupScript : MonoBehaviour
{
    public ArrowCount count;
    GameObject closestArrow;

    private Vector3 position, diff;
    public Transform target;

    public Text arrowPickupText;
    public Camera cam;

    private void Update()
    {
        arrowPickupText.text = "Press F To Pick Up " + FindClosestArrow().gameObject.tag;

        if (!Input.GetKey(KeyCode.Mouse1))
        {
            arrowPickupText.enabled = true;
            arrowPickupText.transform.position = cam.WorldToScreenPoint(FindClosestArrow().transform.position);
        }
        else
        {
            arrowPickupText.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F) && other.tag == "Arrow")
        {
            closestArrow = FindClosestArrow();

            Destroy(closestArrow);

            count.arrowCount += 1;
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
