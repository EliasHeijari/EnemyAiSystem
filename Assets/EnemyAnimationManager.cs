using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyMovementHandler enemyMovementHandler;

    private void Update()
    {
        animator.SetFloat("Speed", enemyMovementHandler.Speed);
    }
}
