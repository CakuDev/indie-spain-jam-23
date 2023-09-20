using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public bool IsAttacking { get; private set; } = false;


    public void Attack()
    {
        if(!IsAttacking)
        {
            IsAttacking = true;
            animator.Play("Attack");
        }
    }

    public void EndAttack()
    {
        IsAttacking = false;
    }
}
