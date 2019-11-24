using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileProjectileLauncher : ProjectileLauncher
{
    public Transform SpawnTransform;

    [HideInInspector]
    public GameObject ProjectilePrefeb;

    [HideInInspector]
    public float Speed;

    [HideInInspector]
    public Vector3 Target;

    public override void Fire()
    {
        var go = Instantiate(ProjectilePrefeb, SpawnTransform.position, Quaternion.identity);

        var body = go.GetComponent<Rigidbody>();
        var direction = Target - transform.position;

        body.AddForce(direction.normalized * Speed);

        base.Fire();
    }
}
