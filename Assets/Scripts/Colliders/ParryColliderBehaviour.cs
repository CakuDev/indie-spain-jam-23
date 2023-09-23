using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryColliderBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If collision with an AttackColliderBehaviour of an enemy
        if(collision.transform.parent.CompareTag("Enemy") && collision.TryGetComponent(out AttackColliderBehaviour attackColliderBehaviour))
        {
            attackColliderBehaviour.transform.parent.GetComponent<EnemyController>().OnParried();
        }
    }
}
