using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackHandler : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Enemy enemy;

    private float attackRotateSpeed = 5f;

    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
    }
    public void Attack(Transform target){
        // Stop moving
        navMeshAgent.velocity = Vector3.zero;

        RotateTowardsTarget(target);

        // attack logic here
        
    }

    private void RotateTowardsTarget(Transform target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        // Look Towards The Target
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, attackRotateSpeed * Time.deltaTime);

    }

}
