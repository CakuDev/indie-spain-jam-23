using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [HideInInspector]
    public InteractiveObjectBehaviour interactiveObject;

    public float healOrDamagingLife;

    public bool canInteract = true;

    public void Interact()
    {
        Debug.Log("INTERACTING!");
        if (!canInteract || interactiveObject == null) return;
        
        interactiveObject.OnInteract(this);
    }
}
