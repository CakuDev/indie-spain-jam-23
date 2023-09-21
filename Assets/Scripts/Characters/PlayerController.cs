using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private MovementBehaviour movementBehaviour;
    [SerializeField] private AttackBehaviour attackBehaviour;

    private void Start()
    {
        // Subscribe to input events
        input.MoveEvent += HandleMove;
        input.InteractEvent += HandleInteract;
        input.AttackEvent += HandleAttack;
    }

    private void HandleInteract()
    {
        Debug.Log("I interact");
    }

    private void HandleMove(Vector2 dir)
    {
        movementBehaviour.Direction = dir;
    }

    private void HandleAttack()
    {
        movementBehaviour.CanMove = false;
        attackBehaviour.Attack();
    }


    // Called from the "Attacking" animation
    private void ManageEndAttack()
    {
        movementBehaviour.CanMove = true;
        attackBehaviour.EndAttack();
    }
}
