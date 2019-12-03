using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbGrip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           var climb = other.gameObject.GetComponent<Climbing>();

            climb.isClimbing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var climb = other.gameObject.GetComponent<Climbing>();

            climb.isClimbing = false;
        }
    }
}
