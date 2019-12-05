using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoader : MonoBehaviour
{

    public AudioSource[] musicSources;
    public int musicBPM, timesignature, barsLength;

    private float LoopPointMinutes, LoopPointSeconds;
    private double Time;
    private int NextSource;
    // Start is called before the first frame update
    void Start()
    {
        LoopPointMinutes = (barsLength * timesignature);// / musicBPM;

        LoopPointSeconds = LoopPointMinutes * 60;

        Time = AudioSettings.dspTime;
        musicSources[0].Play();
        NextSource = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!musicSources[NextSource].isPlaying)
        {
            Time = Time + LoopPointSeconds;
            musicSources[NextSource].PlayScheduled(Time);

            NextSource = 1 - NextSource;//changes to the next audio sourche can add more sounds aswell
        }
    }
}
