using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [HideInInspector] public bool canParry = true;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip parryAudio;


    public void Parry()
    {
        // Avoid attack logic if can't do it
        if (!canParry) return;

        canParry = false;
        audioSource.PlayOneShot(parryAudio);
        animator.SetBool("parry", true);
    }

    public void EndParry()
    {
        canParry = true;
        animator.SetBool("parry", false);
    }
}
