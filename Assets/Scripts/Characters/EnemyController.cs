using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AttackableController
{
    [SerializeField] private MovementBehaviour movementBehaviour;
    [SerializeField] private AttackBehaviour attackBehaviour;
    [SerializeField] private InteractBehaviour interactBehaviour;
    [SerializeField] private Rigidbody2D enemyRb;
    [SerializeField] private float climbingSpeed = 2f;
    [SerializeField] private float walkingSpeed = 4f;

    private float climbHeight;
    public EnemyStatus status;
    public FloorController currentFloor;
    private bool shouldInteract = true;

    private void Update()
    {
        //if (animator.GetBool("death") || animator.GetBool("parried")) return;

        //// After taking a decision, check if the status must change
        //if (ShouldChasePlayer())
        //{
        //    status = EnemyStatus.MOVING;
        //    // Move left or right depending on the enemy and player positions
        //    movementBehaviour.canMove = true;
        //    int direction = transform.position.x > PlayerController.Instance.transform.position.x ? -1 : 1;
        //    movementBehaviour.direction = new(direction, 0);
        //} else if (ShouldDestroyCurrentRepairableItem())
        //{
        //    movementBehaviour.canMove = false;
        //    status = EnemyStatus.DESTROYING;
        //    if (shouldInteract)
        //    {
        //        interactBehaviour.Interact();
        //        shouldInteract = false;
        //        Invoke(nameof(EnableInteraction), 1f);
        //    }
        //} else if (ShouldMoveToCurrentRepairableItem())
        //{
        //    status = EnemyStatus.MOVING;
        //    // Move left or right depending on the enemy and repairable item positions
        //    movementBehaviour.canMove = true;
        //    int direction = transform.position.x > currentFloor.repairableItem.transform.position.x ? -1 : 1;
        //    movementBehaviour.direction = new(direction, 0);
        //}

        //// If the enemy was hitting the player and he is death, ignore him
        //if (status == EnemyStatus.ATTACKING && PlayerController.Instance.currentLife <= 0)
        //{
        //    status = EnemyStatus.MOVING;
        //}

        //if (status == EnemyStatus.DESTROYING)
        //{
        //    animator.SetBool("interact", true);
        //    animator.SetBool("walk", false);
        //    animator.SetBool("attack", false);
        //    animator.SetBool("parried", false);
        //} else { 
        //    animator.SetBool("interact", false);
        //}

        //switch (status)
        //{
        //    case EnemyStatus.ATTACKING:
        //        attackBehaviour.Attack();
        //        break;
        //}
    }

    private void FixedUpdate()
    {
        if (animator.GetBool("death") || animator.GetBool("parried")) return;
        
        switch(status)
        {
            case EnemyStatus.CLIMBING:
                Climb();
                break;
        }
    }

    public void OnSpawn(float height, FloorController floorController)
    {

        // Prepare to climb
        climbHeight = height;
        status = EnemyStatus.CLIMBING;
        movementBehaviour.speed = climbingSpeed;
        movementBehaviour.direction = Vector2.up;
        currentFloor = floorController;

        // Init life counter
        ResetLife();
    }

    private void Climb()
    {
        if (transform.position.y < climbHeight) return;
        
        // Change to moving status, apply gravity and constraint rotation
        //status = EnemyStatus.MOVING;
        enemyRb.bodyType = RigidbodyType2D.Dynamic;
        enemyRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        // Change speed and move into the window depending on his facing direction
        movementBehaviour.speed = walkingSpeed;
        movementBehaviour.direction = transform.localScale.x == 1 ? Vector2.right : Vector2.left;
        animator.SetBool("climb", false);
    }

    // Called in the DetectPlayerBehaviour onEnterTrigger
    public void OnPlayerCollisionEnter() {
        if (animator.GetBool("death")) return;

        // Ignore player if he's death
        if (PlayerController.Instance.currentLife <= 0) return;

        movementBehaviour.canMove = false;
        status = EnemyStatus.ATTACKING;
    }

    // Called in the DetectPlayerBehaviour onExitTrigger
    public void OnPlayerCollisionExit()
    {
       // status = EnemyStatus.MOVING;
    }

    public void ManageEndAttack()
    {
        // Enable attack and check if the player has moved
        attackBehaviour.canAttack = true;

        // The status while be ATTACKING while the player is in the Detect Player Collider
        if (status == EnemyStatus.ATTACKING)
        {
            attackBehaviour.Attack();
        } else
        {
            attackBehaviour.EndAttack();
            movementBehaviour.canMove = true;
        }
    }

    public bool ShouldChasePlayer()
    {
        // If it's not attacking nor climbing, the player is in the same floor and player is alive
        return status != EnemyStatus.ATTACKING
            && status != EnemyStatus.CLIMBING
            && currentFloor == PlayerController.Instance.currentFloor
            && PlayerController.Instance.currentLife > 0;
    }

    public bool ShouldMoveToCurrentRepairableItem()
    {
        // If it's not attacking nor climbing, the player is in the same floor and player is alive
        return status != EnemyStatus.ATTACKING
            && status != EnemyStatus.CLIMBING
            && currentFloor.repairableItem.life > 0;
    }

    public bool ShouldDestroyCurrentRepairableItem()
    {
        return status != EnemyStatus.ATTACKING
            && status != EnemyStatus.CLIMBING
            && currentFloor.repairableItem.life > 0
            && interactBehaviour.interactiveObject != null
            && interactBehaviour.interactiveObject is RepairableItemBehaviour;
    }

    protected override void ManageHit()
    {
        // End any action and block them
        attackBehaviour.EndAttack();
        movementBehaviour.canMove = false;
        interactBehaviour.canInteract = false;
        attackBehaviour.canAttack = false;

        animator.SetBool("hit", true);
    }

    protected override void ManageDeath()
    {
        // End any action and block them
        attackBehaviour.EndAttack();
        movementBehaviour.canMove = false;
        interactBehaviour.canInteract = false;
        attackBehaviour.canAttack = false;

        Debug.Log("DEATH!");
        animator.SetBool("death", true);
        canBeHit = false;
    }

    // Call in the Death animation
    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    public void OnParried()
    {
        // End any action and block them
        attackBehaviour.EndAttack();
        movementBehaviour.canMove = false;
        interactBehaviour.canInteract = false;
        attackBehaviour.canAttack = false;

        animator.SetBool("parried", true);
    }

    public void ManageEndParried()
    {
        movementBehaviour.canMove = true;
        interactBehaviour.canInteract = true;
        attackBehaviour.canAttack = true;

        animator.SetBool("parried", false);
    }

    public void EnableInteraction()
    {
        shouldInteract = true;
    }
}
