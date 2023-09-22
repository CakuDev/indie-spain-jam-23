using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerColliderBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) enemyController.OnPlayerCollisionEnter();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) enemyController.OnPlayerCollisionExit();
    }
}
