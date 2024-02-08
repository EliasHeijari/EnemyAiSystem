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
        navMeshAgent.velocity = Vector3.zero;
        Debug.Log($"Shoot to {target.name}");
    }
}
