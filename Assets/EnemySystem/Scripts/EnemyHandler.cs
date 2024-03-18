using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    private enum State{
        Patrol,
        Chase,
        Attack
    }
    State state;
    Enemy enemy;
    public EnemyAttackHandler attackHandler {get; private set;}
    public EnemyMovementHandler movementHandler {get; private set;}

    [Header("Detection Settings")]
    [Space(15)]
    [SerializeField] LayerMask playerLayer;
    [SerializeField] private float chaseRange = 8f;
    [SerializeField] private float attackRange = 4f;
    [SerializeField] private Vector3 attackRangeOffset = Vector3.zero;
    [SerializeField] private float minimumDetectionRadiusAngle = -40f;
    [SerializeField] private float maximumDetectionRadiusAngle = 65f;

    Vector3 playerLastSeenPos;

    // For Drawing Gizmos
    Vector3 lastSeenPosDraw;
    
    private void Awake() {
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
                movementHandler.Chase(GetPlayerFromChaseRange().transform);
                break;
            case State.Attack:
                attackHandler.Attack(GetPlayerFromChaseRange().transform);
                break;
        }
        // Move to where player last seen
        if (playerLastSeenPos != Vector3.zero)
            movementHandler.MoveTo(playerLastSeenPos, true);
    }

    private void UpdateState()
    {
        if (PlayerOnChaseRange() && IsPlayerOnSight())
        {
            state = State.Chase;
            if (PlayerOnAttackRange())
            {
                state = State.Attack;
            }
        }
        else
        {
            if (state == State.Chase || state == State.Attack)
            {
                playerLastSeenPos = GetPlayerFromChaseRange().transform.position;
                lastSeenPosDraw = playerLastSeenPos;
            }
            else playerLastSeenPos = Vector3.zero;

            state = State.Patrol;
        }

    }

    private bool PlayerOnChaseRange(){
        if (Physics.CheckSphere(transform.position, chaseRange, playerLayer))
        {
            return true; 
        }
        return false;
    }
    private bool PlayerOnAttackRange(){
        if (Physics.CheckSphere(transform.position + attackRangeOffset, attackRange, playerLayer))
        {
            return true;
        }
        return false;
    }
    
    private bool IsPlayerOnSight(){

        if (!PlayerOnChaseRange()) return false;

        Player player = GetPlayerFromChaseRange();

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Vector3 targetDirection = transform.position - player.transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, -transform.forward);

        float characterHeight = 2.5f;

        // Raycast now won't start from the floor
        Vector3 playerStartPoint = new Vector3(player.transform.position.x, characterHeight, player.transform.position.z);
        Vector3 enemyStartPoint = new Vector3(transform.position.x, characterHeight, transform.position.z);

        Debug.DrawLine(playerStartPoint, enemyStartPoint, Color.yellow);

        bool isOnSight = !Physics.Linecast(playerStartPoint, enemyStartPoint, gameObject.layer) && 
            distanceToPlayer <= chaseRange && 
            viewableAngle > minimumDetectionRadiusAngle && viewableAngle < maximumDetectionRadiusAngle;

        return isOnSight;
    }

    private Player GetPlayerFromChaseRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, chaseRange, playerLayer);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Player player))
            {
                return player;
            }
        }
        return null;
    }


    private void OnDrawGizmosSelected() {
        // Draw a yellow sphere, chase Range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
    private void OnDrawGizmos() {

        // Draw a red sphere, attack Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + attackRangeOffset, attackRange);

        // Draw Player Last Seen Position
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(lastSeenPosDraw, 1f);
    }
}
