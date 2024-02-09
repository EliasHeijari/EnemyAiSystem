using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackHandler : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Enemy enemy;
    [SerializeField] private Transform shootingPoint;

    private float shootingDistance = 100f;
    private float shootRotationSpeed = 4f;

    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
    }
    public void Shoot(Transform target){
        // Stop moving
        navMeshAgent.velocity = Vector3.zero;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        // Look Towards The Target
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, shootRotationSpeed * Time.deltaTime);

        // ShootingLogicHere
        if ( Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hit, shootingDistance) )
        {
            // Hitted Something
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(enemy.damage);
                Debug.Log("Hitted damageable object");
            }
        }


    }

}
