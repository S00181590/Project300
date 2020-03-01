using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackScript : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;

    void Start()
    {
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            rb.isKinematic = false;
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * 5000);
        }
    }
}
