using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderBehaviour : MonoBehaviour
{
    [SerializeField] private string tagToHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.parent.CompareTag("Player")) Debug.Log(collision.name);
        if (collision.CompareTag(tagToHit) && collision.TryGetComponent(out AttackableController attackableController))
        {
            attackableController.OnHit();
        }
    }
}
