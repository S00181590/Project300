using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFrustumHider : MonoBehaviour
{
    bool active;
    Vector3 scale;

    void Start()
    {
        scale = new Vector3(100000, 100000, 100000);
    }

    private void Update()
    {
        transform.localScale = scale;

        if (transform.localScale == scale)
        {
            scale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 10)
        {
            if (other.GetComponent<MeshRenderer>() != null)
                other.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 10)
        {
            if (other.GetComponent<MeshRenderer>() != null)
                other.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
