using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemytrackinAndFacesPlayer : NavMeshMover
{
    protected GameObject MainPlayer;
    public float FollowDistance;
    public Vector3 startPoistion;
    float distance = 0;

    public Transform target;

    public override void Start()
    {
        startPoistion = transform.position;
        MainPlayer = GameObject.Find("PlayerMoveController");
         
        base.Start();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, MainPlayer.transform.position) <= FollowDistance)
        {
            agent.stoppingDistance = 3f;
            MoveTo(MainPlayer);
        }
        else
        {
            agent.stoppingDistance = 5;
            MoveTo(startPoistion);
        }

        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();//calling face target to update what the enemy looking at
        }

    }

    void FaceTarget() // makes the enemey or abjecht face the player when he attacks 
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion FollowDistance = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = FollowDistance;
        //transform.rotation = Quaternion.Slerp(transform.rotation, FollowDistance, Time.deltaTime * 5f);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;//color for the enemy follow distance raidus 
        Gizmos.DrawWireSphere(transform.position, FollowDistance);//shows the size of the enemys dection 
    }
}

