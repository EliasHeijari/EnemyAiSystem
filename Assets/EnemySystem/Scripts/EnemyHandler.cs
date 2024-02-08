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
    [SerializeField] private float chaseRange = 8f;
    [SerializeField] private float attackRange = 4f;
    [SerializeField] private float minimumDetectionRadiusAngle = -40f;
    [SerializeField] private float maximumDetectionRadiusAngle = 65f;
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
                movementHandler.Chase(Player.Instance.transform);
                break;
            case State.Attack:
                attackHandler.Shoot(Player.Instance.transform);
                break;
        } 
        Debug.Log(state.ToString());
    }

    private void UpdateState()
    {
        if (TargetOnChaseRange() && IsPlayerOnSight())
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
    
    private bool IsPlayerOnSight(){
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        Vector3 targetDirection = transform.position - Player.Instance.transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, -transform.forward);

        float characterHeight = 2.5f;
        // Raycast now won't start from the floor
        Vector3 playerStartPoint = new Vector3(Player.Instance.transform.position.x, characterHeight, Player.Instance.transform.position.z);
        Vector3 enemyStartPoint = new Vector3(transform.position.x, characterHeight, transform.position.z);

        Debug.DrawLine(playerStartPoint, enemyStartPoint, Color.yellow);

        bool isOnSight = !Physics.Linecast(playerStartPoint, enemyStartPoint, gameObject.layer) && 
            distanceToPlayer <= chaseRange && 
            viewableAngle > minimumDetectionRadiusAngle && viewableAngle < maximumDetectionRadiusAngle;

        return isOnSight;
    }

    private void OnDrawGizmosSelected() {
        // Draw a yellow sphere, chase Range
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, chaseRange);
        // Draw a red sphere, attack Range
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
    }
}
