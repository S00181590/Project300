using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPathFollow : NavMeshMover
{

    public PathNode CurrentNode;
    public float FollowDistance;

    public override void Start()
   {
        base.Start();
        MoveToPathNode();
   }
    private void MoveToPathNode()
    {
        if(CurrentNode != null)
        {
            MoveTo(CurrentNode.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PathNode" && other.gameObject.name == CurrentNode.gameObject.name)
        {
            CurrentNode = other.gameObject.GetComponent<PathNode>().NextNode;
            MoveToPathNode();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;//color for the enemy follow distance raidus 
        Gizmos.DrawWireSphere(transform.position, FollowDistance);//shows the size of the enemys dection 
    }
}
