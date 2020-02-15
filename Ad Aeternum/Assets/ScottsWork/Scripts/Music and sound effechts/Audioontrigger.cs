using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioontrigger : MonoBehaviour
{
    public float num;
    public GameObject trigger;
    public AudioSource audioclip;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Ground")
        {
            trigger.SetActive(true);
            audioclip.Play();
        }
    }
}
