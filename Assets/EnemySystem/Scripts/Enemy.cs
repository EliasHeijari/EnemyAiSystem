using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent navMeshAgent {get; private set;}
    [SerializeField] private Transform[] patrolPoints;
    public Vector3[] PatrolPoints 
    {
        get
        {
            Vector3[] points = new Vector3[patrolPoints.Length];
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                points[i] = patrolPoints[i].position;
            }
            return points;
        }
    }
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
