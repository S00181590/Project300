using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagePlayer : MonoBehaviour
{
    public HealthStaminaScript health;
    public float damage = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            health.value -= damage;
            health.canIncrease = false;
            health.InvokeRepeating("Increase", 2, 2000);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Projectile")
    //    {
    //        health.value -= damage;
    //        health.canIncrease = false;
    //        health.InvokeRepeating("Increase", 2, 2000);
    //    }
    //}
}
