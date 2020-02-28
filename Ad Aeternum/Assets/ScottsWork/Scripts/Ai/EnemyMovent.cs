using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovent : NavMeshMover
{

    public GameObject Player;
    public float FollowAmount;
    public Vector3 startPoistion;
    // Start is called before the first frame update
  public override  void Start()
    {
        startPoistion = transform.position;
        Player = GameObject.Find("Player");
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,Player.transform.position) <= FollowAmount)
        {
            agent.stoppingDistance = 3.0f;
            transform.LookAt(2 * transform.position + startPoistion);
            MoveTo(Player);
        }
        else
        {
            agent.stoppingDistance = 5;
            MoveTo(startPoistion);
        }
    }
}
