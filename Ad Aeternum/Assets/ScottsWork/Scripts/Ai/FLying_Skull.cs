using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLying_Skull : MonoBehaviour
{

    //public float Health = 10, damage = 20;
    public bool PlayParticles = false;
    public  ParticleSystem particleSystem;
    ParticleSystem instantiate;
    public GameObject particlesobjecht;

    public GameObject Explosin;
    public AudioClip sound;
    public float soundVolume = 1;
    public Transform explosin;
    public Transform Spawn;


    private void Start()
    {
        //ParticleSystem.Stop();
    }

    private void Update()
    {
         if(PlayParticles)
        {
            if(!particleSystem.isPlaying)
            {
                instantiate = Instantiate(particleSystem);
                instantiate.transform.position = gameObject.transform.position;
                particleSystem.Play();
            }
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        sound
    //    }
    //    if (other.tag != "Player")
    //        return;

    //    if (sound)
    //        AudioSource.PlayClipAtPoint(sound, transform.position, soundVolume);
    //    Destroy(gameObject);

    //    if (explosin)
    //        Instantiate(explosin, transform.position, transform.rotation);

    //    if (Spawn)
    //        Instantiate(Spawn, transform.position, transform.rotation);
    //}

}
