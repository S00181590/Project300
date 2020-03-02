using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioontrigger : MonoBehaviour
{
    GameObject Player;
      public AudioClip audiotriggerclip;
       AudioSource audioSource;


     void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
     void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (audiotriggerclip != null)
        {
            audioSource.PlayOneShot(audiotriggerclip, 0.5f);
           
        }
    }
}
