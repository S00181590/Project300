using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript3 : MonoBehaviour
{
    GameObject sphere;
    void Start()
    {
        sphere = this.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Destroy(other.gameObject);
            }
        }
    }

    //public void OnCollisionStay(Collision other)
    //{

    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        if (Input.GetKeyDown(KeyCode.Mouse0))
    //        {
    //            Destroy(other.gameObject);
    //        }
    //    }
    //}
}
