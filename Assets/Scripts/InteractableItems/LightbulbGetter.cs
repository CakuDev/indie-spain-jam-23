using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightbulbGetter : InteractiveObjectBehaviour
{
    public LightLifeController lightLifeController;
    public AudioSource audioSource;
    public AudioClip pickUpAudio;
    public Animator animator;

    private void Update()
    {
        keyToPress.SetActive(showKey && lightLifeController.damagedLightbulb && !lightLifeController.hasLightbulb);
        animator.SetBool("lightBroken", lightLifeController.damagedLightbulb);
        animator.SetBool("alreadyPickUp", lightLifeController.hasLightbulb);
    }

    public override void OnInteract(InteractBehaviour interactBehaviour)
    {
        
        if (lightLifeController.damagedLightbulb && !lightLifeController.hasLightbulb)
        {
            lightLifeController.hasLightbulb = true;
            audioSource.PlayOneShot(pickUpAudio);
        }
    }

    
}
