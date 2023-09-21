using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;

    //[HideInInspector] public interactiveObjectBehaviour InteractiveObject;
    [HideInInspector] public bool canInteract = true;

    public void Interact()
    {
        // TODO
        if (!canInteract) return;

        Debug.Log("Interact!");
    }
}
