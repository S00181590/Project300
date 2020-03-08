using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemytacking : NavMeshMover
{
    public GameObject PlayerChracther;
    public float FollowDistance;
    public Vector3 startPoistion;

   public override void Start()
    {
        startPoistion = transform.position;
        PlayerChracther = GameObject.Find("PlayerMoveController");
        base.Start();
    }

    
   private void Update()
    {
        if(Vector3.Distance(transform.position,PlayerChracther.transform.position)<=FollowDistance)
        {
            agent.stoppingDistance = 2;
            MoveTo(PlayerChracther);
        }

        else
        {
            agent.stoppingDistance = 0;
            MoveTo(startPoistion);
        }
    }
}
