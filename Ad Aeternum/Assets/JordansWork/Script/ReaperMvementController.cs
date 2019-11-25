using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReaperMvementController : EnemyMover
{
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        PlayerCharacter = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
    }

    //Update is called once per frame
    void Update()
    {
        MoveTo(PlayerCharacter);
    }
}
