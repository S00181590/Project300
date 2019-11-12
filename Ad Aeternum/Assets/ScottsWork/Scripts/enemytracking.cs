using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytracking : NavMeshMover
{

    public GameObject PlayerChacther;
    public float TrackDistance;
    public Vector3 startingPoistion;
    
   public override void Start()
    {
        startingPoistion = transform.position;
        base.Start();
    }

    // Update is called once per frame
   private void Update()
    {
        if(Vector3.Distance(transform.position, PlayerChacther.transform.position)
            <= TrackDistance)
        {
           // agent.
            
        }
    }
}
