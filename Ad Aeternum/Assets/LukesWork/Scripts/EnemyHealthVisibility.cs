using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthVisibility : MonoBehaviour
{
    Canvas healthBar;

    void Start()
    {
        healthBar = gameObject.GetComponentInChildren<Canvas>();
        healthBar.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            healthBar.enabled = true;
            CancelInvoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            healthBar.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            CancelInvoke();
            healthBar.enabled = true;
            Invoke("Hide", 2);
        }
    }

    void Hide()
    {
        healthBar.enabled = false;
    }
}
