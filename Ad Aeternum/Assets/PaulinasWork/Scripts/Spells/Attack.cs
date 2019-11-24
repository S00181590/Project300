using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Attack : ScriptableObject
{
    public string Name;
    public float Damage;
    public float Cost;
    public ResourceSource Source;

    public abstract void Initialize(GameObject caster, Vector3 target);

    public abstract void Cast();
}

[Serializable]
public enum ResourceSource
{ Health, Stamina, Recharge }

[CreateAssetMenu(fileName="Projectile Attack", menuName ="Projectile Attack")]
public class ProjectileAttack : Attack
{
    ProjectileProjectileLauncher launcher;

    public GameObject ProjectilePrefab;

    public float Speed;

    public override void Cast()
    {
        launcher.Fire();
    }

    public override void Initialize(GameObject caster, Vector3 target)
    {
        launcher = caster.GetComponent<ProjectileProjectileLauncher>();
        launcher.Speed = Speed;
        launcher.ProjectilePrefeb = ProjectilePrefab;
        launcher.Target = target;

    }
}