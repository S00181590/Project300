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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            rb.isKinematic = false;
            rb.AddForce(other.transform.position * 50, ForceMode.Impulse);
        }
    }
}
