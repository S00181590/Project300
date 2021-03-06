﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : NavMeshMover {

    protected GameObject PlayerCharacter;
    public float FollowDistance;
    public Vector3 startPosition;
    public float dis = 2.5f;

    public override void Start()
    {
        startPosition = transform.position;
        PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        base.Start();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerCharacter.transform.position) <= FollowDistance)
        {
            agent.stoppingDistance = dis;
            MoveTo(PlayerCharacter);
        }
        else
        {
            agent.stoppingDistance = 5;
            MoveTo(startPosition);
        }
    }
}
