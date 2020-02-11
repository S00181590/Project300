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
    float playerSpeed, distance, rand;

    void Start()
    {
        enemy = this.gameObject;
    }

    void Update()
    {
        playerVelocity = player.GetComponent<Rigidbody>().velocity;
        playerSpeed = playerVelocity.magnitude;

        distance = Vector3.Distance(enemy.transform.position, player.transform.position);

        rand = Random.Range(2.0f, 4.0f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy.transform.LookAt(Vector3.Lerp(Vector3.forward, player.transform.position + player.transform.forward * (playerSpeed * (distance * 0.02f)), 1));

            Invoke("Shoot", rand);
        }
    }

    void Shoot()
    {
        instProjectile = Instantiate(projectile, enemy.transform.position, Quaternion.identity);

        rb = instProjectile.GetComponent<Rigidbody>();

        rb.AddForce((enemy.transform.forward * (distance * 3)/*) + (enemy.transform.up * (distance / 10))*/), ForceMode.Impulse);

        Destroy(instProjectile, 2);

        CancelInvoke();
    }
}
