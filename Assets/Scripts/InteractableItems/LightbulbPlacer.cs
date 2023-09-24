using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightbulbPlacer : InteractiveObjectBehaviour
{

    public LightLifeController lightLifeController;


    public override void OnInteract(InteractBehaviour interactBehaviour)
    {
        if (lightLifeController.damagedLightbulb && lightLifeController.hasLightbulb)
        {
            lightLifeController.RestoreLightBulb();
            Debug.Log("Lightbulb correctly changed");
        }
    }

}
