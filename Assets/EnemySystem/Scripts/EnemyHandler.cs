using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    private enum State{
        Patrol,
        Chase,
        Attack
    }
    Enemy enemy;
    State state;
    [SerializeField] LayerMask playerLayer;
    EnemyAttackHandler attackHandler;
    EnemyMovementHandler movementHandler;
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange = 8f;
    [SerializeField] private float attackRange = 4f;
    private void Start() {
        enemy = GetComponent<Enemy>();
        attackHandler = GetComponent<EnemyAttackHandler>();
        movementHandler = GetComponent<EnemyMovementHandler>();
    }
    private void Update() {
        UpdateState();
        switch(state){
            case State.Patrol:
                movementHandler.Patrol();
                break;
            case State.Chase:
                movementHandler.Chase(target);
                break;
            case State.Attack:
                attackHandler.Shoot(target);
                break;
        } 
        Debug.Log(state.ToString());
    }

    private void UpdateState()
    {
        if (TargetOnChaseRange())
        {
            state = State.Chase;
            if (TargetOnAttackRange())
            {
                state = State.Attack;
            }
        }
        else
        {
            state = State.Patrol;
        }

    }

    private bool TargetOnChaseRange(){
        if (Physics.CheckSphere(transform.position, chaseRange, playerLayer))
        {
            return true; 
        }
        return false;
    }
    private bool TargetOnAttackRange(){
        if (Physics.CheckSphere(transform.position, attackRange, playerLayer))
        {
            return true; 
        }
        return false;
    }
}
