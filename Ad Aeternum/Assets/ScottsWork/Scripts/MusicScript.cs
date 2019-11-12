using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{

    public AudioSource musicSource;
    public AudioClip MusicStart;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.PlayOneShot(MusicStart);
        musicSource.PlayScheduled(AudioSettings.dspTime + MusicStart.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
