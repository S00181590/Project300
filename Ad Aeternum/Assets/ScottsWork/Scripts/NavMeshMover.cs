using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshMover))]
public class NavMeshMover : MonoBehaviour
{
    protected NavMeshMover agent;
    public virtual void Start()
    {
        agent = GetComponent<NavMeshMover>();
    }

    public void MoveTo(Vector3 poistion)
    {
        //agent.SetDestination(poistion);
    }

    public void MoveTo(GameObject gameObject)
    {
        MoveTo(gameObject.transform.position);
    }
    public void Stop()
    {
        //agent.isStopped = true;
    }


}
