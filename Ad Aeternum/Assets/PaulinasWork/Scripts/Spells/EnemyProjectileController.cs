using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageResource))]
[RequireComponent(typeof(HealthResource))]
[RequireComponent(typeof(StaminaResource))]
public class EnemyProjectileController : MonoBehaviour
{
    public Attack activeAttack;
    DamageResource damage;
    HealthResource health;
    StaminaResource stamina;

    Ray ray;
    RaycastHit hitResult;
    Vector3 targetPosition;
    GameObject player;
    float distance = 10f;

    private void Start()
    {
        damage = GetComponent<DamageResource>();
        stamina = GetComponent<StaminaResource>();
        health = GetComponent<HealthResource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        targetPosition = player.transform.position;
        if (Vector3.Distance(transform.position, player.transform.position) <= distance)
        {
            SetTargetAndAttack();
        }
    }

    void SetTargetAndAttack()
    {
        if(Physics.Raycast(ray,out hitResult,Mathf.Infinity))
        {
            targetPosition = hitResult.point;

            activeAttack.Initialize(this.gameObject, targetPosition);
            activeAttack.Cast();
            damage.Value -= activeAttack.Cost;
        }
    }
}
