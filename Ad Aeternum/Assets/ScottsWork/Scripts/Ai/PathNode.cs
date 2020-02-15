using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public PathNode NextNode;
    public Color PathDebugColor;


    private void OnDrawGizmos()
    {
        Gizmos.color = PathDebugColor;
        Gizmos.DrawLine(transform.position, NextNode.transform.position);

        Vector3 direction = NextNode.transform.position - transform.position;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (direction * 0.5f));
    }
}
