using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReaperMvementController : EnemyMover
{
    public ReaperBossState.BossState State = ReaperBossState.BossState.Idle;

    public float EvadeDistance = 10f;

    public bool PlayerIsHere;

    //public NavMeshAgent agent;

    public int pattern;

    // Start is called before the first frame update
    void Start()
    {
        PlayerIsHere = false;

        pattern = 1;

        State = ReaperBossState.BossState.Idle;

        startPosition = transform.position;

        PlayerCharacter = GameObject.FindGameObjectWithTag("Player");

        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    //Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, PlayerCharacter.transform.position) > 20)
        {
            PlayerIsHere = false;
        };
        
        if(State == ReaperBossState.BossState.Idle)
        {
            transform.position = startPosition;

            if (PlayerIsHere)
            {
                Invoke("AvoidAction", 0);
            }
        }
        else if(State == ReaperBossState.BossState.ForwardAttack)
        {
            agent.stoppingDistance = 2;

            MoveTo(PlayerCharacter);

            pattern = 2;

            Invoke("AvoidAction", 10);
        }
        else if(State == ReaperBossState.BossState.Evade)
        {
            agent.stoppingDistance = 10f;

            MoveTo(PlayerCharacter);

            if(!PlayerIsHere)
            {
                Invoke("IdleAction", 0);
            }
            else
            {
                if (pattern == 1)
                {
                    Invoke("ForwardAttackAction", 4);
                }
                else if (pattern == 2)
                {
                    Invoke("BackAttackAction", 6);
                }
            }

        }
        else if(State == ReaperBossState.BossState.BackAttack)
        {
            CapsuleCollider reaper = gameObject.GetComponent<CapsuleCollider>();
            NavMeshAgent nav = gameObject.GetComponent<NavMeshAgent>();

            //reaper.enabled = false;

            nav.Warp(PlayerCharacter.transform.position += new Vector3(2, 0, -2));

            //MoveTo(PlayerCharacter);

            pattern = 1;

            Invoke("AvoidAction", 0);

        }
    }

    private void IdleAction()
    {
        State = ReaperBossState.BossState.Idle;
    }
    private void AvoidAction()
    {
        State = ReaperBossState.BossState.Evade;
    }
    private void ForwardAttackAction()
    {
        State = ReaperBossState.BossState.ForwardAttack;
    }
    private void BackAttackAction()
    {
        State = ReaperBossState.BossState.BackAttack;
    }

    private bool isCoroutineExecuting = false;

    IEnumerator ExecuteAfterTime(float time)
    {
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        // Code to execute after the delay

        isCoroutineExecuting = false;
    }
}
