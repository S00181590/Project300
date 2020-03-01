using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFrustumHider : MonoBehaviour
{
    bool active;
    Vector3 scale;
    public bool terrainCull;

    void Start()
    {
        scale = new Vector3(100000, 100000, 100000);
        scale = new Vector3(1, 1, 1);
    }

    //private void Update()
    //{
    //    transform.localScale = scale;

    //    if (transform.localScale == scale)
    //    {
    //        scale = new Vector3(1, 1, 1);
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (terrainCull == false)
        {
            if (other.gameObject.layer != 10)
            {
                //if (other.GetComponent<MeshRenderer>() != null)
                //    other.GetComponent<MeshRenderer>().enabled = true;

                if (/*other.gameObject != null && */other.GetComponent<MeshRenderer>() != null)
                {
                    other.GetComponent<MeshRenderer>().enabled = true;
                    //other.enabled = true;
                }

                if (other.GetComponent<ParticleSystem>() != null)
                {
                    var emission = other.GetComponent<ParticleSystem>().emission;
                    emission.enabled = true;
                }
            }
        }
        else
        {
            if (other.gameObject.layer == 10)
            {
                if (other.GetComponent<MeshRenderer>() != null)
                {
                    other.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            
            if (other.gameObject.layer == 10)
            {
                if (other.GetComponent<MeshRenderer>() != null)
                {
                    other.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 10)
        {
            //if (other.GetComponent<MeshRenderer>() != null)
            //    other.GetComponent<MeshRenderer>().enabled = false;

            if (/*other.gameObject != null && */other.GetComponent<MeshRenderer>() != null)
            {
                other.GetComponent<MeshRenderer>().enabled = false;
                //other.enabled = false;
            }

            if (other.GetComponent<ParticleSystem>() != null)
            {
                var emission = other.GetComponent<ParticleSystem>().emission;
                emission.enabled = false;
            }
        }
    }
}
