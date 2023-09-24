using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject attackCollider;

    [HideInInspector] public bool canAttack = true;

    public void Attack()
    {
        // Avoid attack logic if can't do it
        if (!canAttack) return;

        canAttack = false;

        animator.SetBool("attack", true);
    }

    public void EndAttack()
    {
        canAttack = true;
        animator.SetBool("attack", false);
        //attackCollider.SetActive(false);
    }
}
