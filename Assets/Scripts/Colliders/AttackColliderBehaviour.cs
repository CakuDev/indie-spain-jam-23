using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderBehaviour : MonoBehaviour
{
    private AttackableController attackableController;

    private void Update()
    {
        if (attackableController == null) return;
        Debug.Log("HIT");
        attackableController.OnHit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AttackableController attackableController))
        {
            this.attackableController = attackableController;
            attackableController.OnHit();
        }
    }
}
