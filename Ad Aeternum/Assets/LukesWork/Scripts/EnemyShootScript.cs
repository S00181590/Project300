using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    EnemyMover enemyMover;
    GameObject enemy, instProjectile;
    public GameObject projectile, player;
    Rigidbody rb;

    void Start()
    {
        enemy = this.gameObject;
    }

    void Update()
    {
        InvokeRepeating("Shoot", 2, 5);
    }

    private void OnTriggerStay(Collider other)
    {
        enemy.transform.LookAt(player.transform);
    }

    void Shoot()
    {
        instProjectile = Instantiate(projectile, enemy.transform.position, Quaternion.identity);

        rb = instProjectile.GetComponent<Rigidbody>();

        rb.AddForce(enemy.transform.forward * 5, ForceMode.Impulse);

        Destroy(instProjectile, 2);

        CancelInvoke();
    }
}
