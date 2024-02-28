using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance;

    private int health = 100;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this){
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        // Dying logic here
        Debug.Log("Player Died");
    }
}
