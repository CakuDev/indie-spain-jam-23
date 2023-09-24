using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AttackableController
{
    // SINGLETON
    public static PlayerController Instance { get; private set; }

    [SerializeField] private InputReader input;
    [SerializeField] private MovementBehaviour movementBehaviour;
    [SerializeField] private AttackBehaviour attackBehaviour;
    [SerializeField] private ParryBehaviour parryBehaviour;
    [SerializeField] private InteractBehaviour interactBehaviour;
    [SerializeField] private HoldItemBehaviour holdItemBehaviour;
    
    private void Awake()
    {
        // Singleton logic
        if (Instance != null && Instance != this) Destroy(gameObject);

        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        // Subscribe to input events
        input.MoveEvent += HandleMove;
        input.InteractEvent += HandleInteract;
        input.AttackEvent += HandleAttack;
        input.ParryEvent += HandleParry;
        input.HoldEvent += HandleHold;

        // Init life counter
        ResetLife();
    }

    private void HandleInteract()
    {
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
        canBeHit = false;
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
        canBeHit = true;
        parryBehaviour.EndParry();
    }

    protected override void ManageHit()
    {
        // End any action and block them
        attackBehaviour.EndAttack();
        parryBehaviour.EndParry();
        movementBehaviour.canMove = false;
        interactBehaviour.canInteract = false;
        attackBehaviour.canAttack = false;
        holdItemBehaviour.canHold = false;
        parryBehaviour.canParry = false;
        
        animator.SetBool("hit", true);
        Invoke(nameof(EnableAllActions), 0.6f);
    }

    protected override void ManageDeath()
    {
        // End any action and block them
        attackBehaviour.EndAttack();
        parryBehaviour.EndParry();
        movementBehaviour.canMove = false;
        interactBehaviour.canInteract = false;
        attackBehaviour.canAttack = false;
        holdItemBehaviour.canHold = false;
        parryBehaviour.canParry = false;

        animator.SetBool("death", true);
        Invoke(nameof(EnableAllActions), 2f);
    }

    // Call from the Hit and Death animations
    private void EnableAllActions()
    {
        Debug.Log("ENABLE ALL ACTIONS");

        movementBehaviour.canMove = true;
        interactBehaviour.canInteract = true;
        attackBehaviour.canAttack = true;
        holdItemBehaviour.canHold = true;
        parryBehaviour.canParry = true;

        animator.SetBool("hit", false);
        animator.SetBool("death", false);
        canBeHit = true;

        // If comes from death, reset life
        if (currentLife <= 0) ResetLife();
    }
}
