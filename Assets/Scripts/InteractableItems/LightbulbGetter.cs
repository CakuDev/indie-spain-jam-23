using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightbulbGetter : InteractiveObjectBehaviour
{
    public LightLifeController lightLifeController;


    public override void OnInteract(InteractBehaviour interactBehaviour)
    {
        if (lightLifeController.damagedLightbulb && !lightLifeController.hasLightbulb)
        {
            lightLifeController.hasLightbulb = true;
            Debug.Log("New Lightbulb Acquired");
        }
    }

    
}
