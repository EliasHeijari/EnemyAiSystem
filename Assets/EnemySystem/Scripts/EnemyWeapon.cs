using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject bloodSprayParicle;
    Vector3 hittedPosition;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            hittedPosition = collision.contacts[0].point;
            player.TakeDamage(enemy.damage);
            Quaternion bloodSprayRotation = Quaternion.LookRotation(collision.contacts[0].normal, Vector3.up);
            GameObject bloodSpray = Instantiate(bloodSprayParicle, hittedPosition, bloodSprayRotation);
            bloodSpray.transform.parent = player.transform;
            Destroy(bloodSpray, 25f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hittedPosition, 0.1f);
    }
}
