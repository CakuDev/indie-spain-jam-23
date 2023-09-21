using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObjectBehaviour : InteractiveObjectBehaviour
{
    public Transform objectToTeleport;

    public Transform cameraPosition;

    //Teleports the character
    public override void OnInteract(InteractBehaviour interactBehaviour)
    {
        interactBehaviour.transform.position = objectToTeleport.position;
        Camera.main.transform.position = new Vector3(cameraPosition.position.x, cameraPosition.position.y, Camera.main.transform.position.z);
    }
}
