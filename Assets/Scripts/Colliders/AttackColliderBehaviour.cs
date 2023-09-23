using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderBehaviour : MonoBehaviour
{
    [SerializeField] private string tagToHit;

    private AttackableController attackableController;

    private void Update()
    {
        if (attackableController == null) return;
        attackableController.OnHit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagToHit) && collision.TryGetComponent(out AttackableController attackableController))
        {
            this.attackableController = attackableController;
            attackableController.OnHit();
        }
    }
}
