using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    EnemyMover enemyMover;
    GameObject enemy, instProjectile;
    public GameObject projectile, player;
    Rigidbody rb;
    Vector3 playerVelocity;
    float playerSpeed, distance;

    void Start()
    {
        enemy = this.gameObject;
    }

    void Update()
    {
        InvokeRepeating("Shoot", 2, 5);

        playerVelocity = player.GetComponent<Rigidbody>().velocity;
        playerSpeed = playerVelocity.magnitude;

        distance = Vector3.Distance(enemy.transform.position, player.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            enemy.transform.LookAt(Vector3.Lerp(Vector3.forward, player.transform.position + player.transform.forward * (playerSpeed * (distance * 0.02f)), 1));
    }

    void Shoot()
    {
        instProjectile = Instantiate(projectile, enemy.transform.position, Quaternion.identity);

        rb = instProjectile.GetComponent<Rigidbody>();

        rb.AddForce(enemy.transform.forward * 50, ForceMode.Impulse);

        Destroy(instProjectile, 2);

        CancelInvoke();
    }
}
