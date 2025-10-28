using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject attackCollider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackAudio;

    public bool canAttack = true;

    public void Attack()
    {
        // Avoid attack logic if can't do it
        if (!canAttack) return;

        canAttack = false;
        audioSource.PlayOneShot(attackAudio);
        animator.SetBool("attack", true);
    }

    public void EndAttack()
    {
        canAttack = true;
        animator.SetBool("attack", false);
    }
}
