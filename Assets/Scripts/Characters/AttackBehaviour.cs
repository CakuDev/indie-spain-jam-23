using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public bool CanAttack { get; private set; } = true;


    public void Attack()
    {
        // Avoid attack logic if can't do it
        if (!CanAttack) return;

        CanAttack = false;

        animator.SetBool("attack", true);
    }

    public void EndAttack()
    {
        CanAttack = true;
        animator.SetBool("attack", false);
    }
}
