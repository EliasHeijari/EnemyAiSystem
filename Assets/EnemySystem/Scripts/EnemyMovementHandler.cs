using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementHandler : MonoBehaviour
{

    NavMeshAgent navMeshAgent;
    Vector3[] patrolPoints;
    Enemy enemy;

    Vector3 targetPos;

    private void Start() {
        enemy = GetComponent<Enemy>();
        navMeshAgent = enemy.navMeshAgent;
        patrolPoints = enemy.PatrolPoints;
    }

    public void Chase(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) > 1f)
        {
        if (navMeshAgent.destination != target.position)
            navMeshAgent.SetDestination(target.position);
        }
        else {
            navMeshAgent.velocity = Vector3.zero;
        }
    }

    public void Patrol()
    {
        if (Vector3.Distance(transform.position, targetPos) > 1f)
        {
            navMeshAgent.SetDestination(targetPos);
        }
        else{
            targetPos = patrolPoints[Random.Range(0, patrolPoints.Length)];
        }
    }

}
