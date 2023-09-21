using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private MovementBehaviour movementBehaviour;
    [SerializeField] private AttackBehaviour attackBehaviour;
    [SerializeField] private ParryBehaviour parryBehaviour;
    [SerializeField] private InteractBehaviour interactBehaviour;
    [SerializeField] private HoldItemBehaviour holdItemBehaviour;
    public float life;

    public bool canChangeFloor;
    public bool canRepairItem;

    private void Start()
    {
        // Subscribe to input events
        input.MoveEvent += HandleMove;
        input.InteractEvent += HandleInteract;
        input.AttackEvent += HandleAttack;
        input.ParryEvent += HandleParry;
        input.HoldEvent += HandleHold;
    }

    private void HandleInteract()
    {
        // TODO
        interactBehaviour.Interact();
    }

    private void HandleHold()
    {
        // TODO
        holdItemBehaviour.HoldOrDropItem();
    }

    private void HandleMove(Vector2 dir)
    {
        movementBehaviour.direction = dir;
    }

    private void HandleAttack()
    {
        movementBehaviour.canMove = false;
        interactBehaviour.canInteract = false;
        parryBehaviour.canParry = false;
        holdItemBehaviour.canHold = false;
        attackBehaviour.Attack();
    }

    private void HandleParry()
    {
        movementBehaviour.canMove = false;
        interactBehaviour.canInteract = false;
        attackBehaviour.canAttack = false;
        holdItemBehaviour.canHold = false;
        parryBehaviour.Parry();
    }


    // Called in the "Attacking" animation
    private void ManageEndAttack()
    {
        movementBehaviour.canMove = true;
        interactBehaviour.canInteract = true;
        parryBehaviour.canParry = true;
        holdItemBehaviour.canHold = true;
        attackBehaviour.EndAttack();
    }

    // Called in the "Parrying" animation
    private void ManageEndParry()
    {
        movementBehaviour.canMove = true;
        interactBehaviour.canInteract = true;
        attackBehaviour.canAttack = true;
        holdItemBehaviour.canHold = true;
        parryBehaviour.EndParry();
    }
}
