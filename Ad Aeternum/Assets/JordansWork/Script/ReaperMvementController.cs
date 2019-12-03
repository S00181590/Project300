using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReaperMvementController : EnemyMover
{
    public ReaperBossState.BossState State = ReaperBossState.BossState.Idle;

    public float EvadeDistance;

    public bool PlayerIsHere;

    // Start is called before the first frame update
    void Start()
    {
        PlayerIsHere = false;

        startPosition = transform.position;

        PlayerCharacter = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
    }

    //Update is called once per frame
    void Update()
    {
        if(State == ReaperBossState.BossState.Idle)
        {

        }
        else if(State == ReaperBossState.BossState.ForwardAttack)
        {
            MoveTo(PlayerCharacter);
        }
        else if(State == ReaperBossState.BossState.Evade)
        {
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();

            agent.stoppingDistance = EvadeDistance;

            MoveTo(PlayerCharacter);
        }
        else if(State == ReaperBossState.BossState.BackAttack)
        {
            CapsuleCollider reaper = gameObject.GetComponent<CapsuleCollider>();
            NavMeshAgent nav = gameObject.GetComponent<NavMeshAgent>();

            reaper.enabled = false;
            nav.speed = 10f;
            
            MoveTo(PlayerCharacter);

        }
    }
}
