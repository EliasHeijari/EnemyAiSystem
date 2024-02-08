using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    public void Shoot(Transform target){
        Debug.Log($"Shoot to {target.name}");
    }
}
