using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementHandler : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private float walkingSpeed = 2f;
    [SerializeField] private float runningSpeed = 3f;

    public float Speed { get { return navMeshAgent.speed; } }

    NavMeshAgent navMeshAgent;
    Vector3[] patrolPoints;
    Enemy enemy;

    Vector3 targetPos;
    bool isMovingTo;

    private void Start() {
        enemy = GetComponent<Enemy>();
        navMeshAgent = enemy.navMeshAgent;
        patrolPoints = enemy.PatrolPoints;
    }

    public void Chase(Transform target)
    {
        navMeshAgent.speed = runningSpeed;
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
        // MoveTo method is called and running, so dont do patrol
        if (isMovingTo) return;

        navMeshAgent.speed = walkingSpeed;

        if (Vector3.Distance(transform.position, targetPos) > 1f)
        {
            navMeshAgent.SetDestination(targetPos);
        }
        else{
            targetPos = patrolPoints[UnityEngine.Random.Range(0, patrolPoints.Length)];
        }
        
    }

    /// <summary>
    /// move to given postion and ignores Patrol even when it's called
    /// </summary>
    /// <param name="position"></param>
    public void MoveTo(Vector3 position, bool running = false)
    {
        navMeshAgent.speed = running ? runningSpeed : walkingSpeed;

        isMovingTo = true;
        StartCoroutine(MoveCoroutine(position));
    }

    private IEnumerator MoveCoroutine(Vector3 position)
    {
        navMeshAgent.SetDestination(position);

        while (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            // You can perform checks or actions while the object is moving

            yield return null;
        }

        // Object has reached the destination
        isMovingTo = false;
    }

}
