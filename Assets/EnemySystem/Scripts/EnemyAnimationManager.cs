using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyHandler enemyHandler;

    private const string SPEED_PARAM = "Speed";
    private const string ATTACK_PARAM = "Attack";

    private void Start() {
        enemyHandler.attackHandler.OnAttack += AttackHandler_OnAttack;
    }

    private void AttackHandler_OnAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK_PARAM);
    }

    private void Update()
    {
        animator.SetFloat(SPEED_PARAM, enemyHandler.movementHandler.Speed);
    }
}
