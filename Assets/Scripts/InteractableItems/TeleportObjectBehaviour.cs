using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObjectBehaviour : InteractiveObjectBehaviour
{
    public Transform objectToTeleport;

    public Transform cameraPosition;

    public float tweeningDuration;

    public FloorController floorToTp;

    private void Start()
    {
        DOTween.Init();
    }

    //Teleports the character
    public override void OnInteract(InteractBehaviour interactBehaviour)
    {
        interactBehaviour.transform.position = objectToTeleport.position;
        Vector3 newPos = new Vector3(cameraPosition.position.x, cameraPosition.position.y, Camera.main.transform.position.z);
        Camera.main.transform.DOMove(newPos, tweeningDuration);

        interactBehaviour.GetComponent<AttackableController>().currentFloor = floorToTp;
    }
}
