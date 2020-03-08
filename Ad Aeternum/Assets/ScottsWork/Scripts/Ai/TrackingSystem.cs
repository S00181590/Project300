using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingSystem : MonoBehaviour
{
    public float Speed =5.0f;
   public GameObject targetselected = null;
    Vector3 LastKnownPoistion = Vector3.zero;//to update rotation to know what to aim at 

    Quaternion lookAtRotation;
    // Update is called once per frame

    private void Start()
    {
        targetselected = GameObject.Find("PlayerMoveController");
        LastKnownPoistion = GameObject.Find("PlayerMoveController").transform.position;
    }

    void Update()
    {
        if (targetselected)
        {

            if (LastKnownPoistion != targetselected.transform.position)
            {
                LastKnownPoistion = targetselected.transform.position;
                lookAtRotation = Quaternion.LookRotation(LastKnownPoistion - transform.position);

            }
            if (transform.rotation != lookAtRotation)
            {


                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtRotation, Speed * Time.deltaTime);

            }
        }
    }

    bool SetTarget(GameObject target)
    {
        if (target)
        {
            return false;
        }
        targetselected = target;
        return true;
    }
}
