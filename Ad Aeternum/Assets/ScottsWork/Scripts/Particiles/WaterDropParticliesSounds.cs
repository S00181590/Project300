using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropParticliesSounds : MonoBehaviour
{
    public AudioClip collisionSFX;

    ParticleSystem partSystem;
    ParticleCollisionEvent[] collisionEvents;

    void Awake()
    {

        partSystem = GetComponent<ParticleSystem>();
        collisionEvents = new ParticleCollisionEvent[16];
    }

    void OnParticleCollision(GameObject other)
    {

        int safeLength = partSystem.GetSafeCollisionEventSize();
        if (collisionEvents.Length < safeLength)
            collisionEvents = new ParticleCollisionEvent[safeLength];

        int totalCollisions = partSystem.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < totalCollisions; i++)
            AudioSource.PlayClipAtPoint(collisionSFX, collisionEvents[i].intersection);

        print(totalCollisions);
    }
}