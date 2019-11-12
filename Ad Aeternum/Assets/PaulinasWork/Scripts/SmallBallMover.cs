using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBallMover : NavMeshMover {

    public GameObject PlayerCharacter;
    public float FollowDistance;
    public Vector3 startPosition;

    public override void Start()
    {
        startPosition = transform.position;
        base.Start();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, PlayerCharacter.transform.position) <= FollowDistance)
        {
            agent.stoppingDistance = 2;
            MoveTo(PlayerCharacter);
        }
        else
        {
            agent.stoppingDistance = 0;
            MoveTo(startPosition);
        }
    }
}
