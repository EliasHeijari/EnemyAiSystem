using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackHandler : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void Shoot(Transform target){
        // Stop moving
        navMeshAgent.velocity = Vector3.zero;
        // Look Towards The Target
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);

        // ShootingLogicHere
    }

}
