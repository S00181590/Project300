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

    public Text arrowPickupText = null;
    public Camera cam;

    private void Update()
    {
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            if (FindClosestArrow() != null && FindClosestArrow().gameObject.GetComponent<Arrow>().collectable == true)
            {
                arrowPickupText.transform.position = cam.WorldToScreenPoint(FindClosestArrow().transform.position);
                arrowPickupText.text = "Press F To Pick Up " + FindClosestArrow().gameObject.tag;
                arrowPickupText.enabled = true;
            }
            else
            {
                arrowPickupText.enabled = false;
            }
        }
        else
        {
            arrowPickupText.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (FindClosestArrow() != null)
            {
                closestArrow = FindClosestArrow();

                Destroy(closestArrow);

                count.arrowCount += 1;
            }
        }
    }

    public GameObject FindClosestArrow()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Arrow");
        GameObject closest = null;
        float distance = 5;
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
