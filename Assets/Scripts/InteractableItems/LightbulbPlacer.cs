using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightbulbPlacer : InteractiveObjectBehaviour
{

    public LightLifeController lightLifeController;

    private void Update()
    {
        keyToPress.SetActive(showKey && lightLifeController.damagedLightbulb && lightLifeController.hasLightbulb);
    }
    public override void OnInteract(InteractBehaviour interactBehaviour)
    {
        if (lightLifeController.damagedLightbulb && lightLifeController.hasLightbulb)
        {
            lightLifeController.RestoreLightBulb();
            Debug.Log("Lightbulb correctly changed");
        }
    }

}
