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
    public EnemyStatus state;
    public FloorController currentFloor;
    private bool playerInAttackRange = false;
    private TeleportObjectBehaviour tpObjectToMove;
    private float attackAnimLength;
    private float parriedAnimLength;
    private float deathAnimLength;

    private void Start()
    {
        foreach(AnimationClip anim in animator.runtimeAnimatorController.animationClips)
        {
            switch(anim.name)
            {
                case "Attacking":
                    attackAnimLength = anim.length;
                    break;

                case "Parried":
                    parriedAnimLength = anim.length;
                    break;

                case "Death":
                    deathAnimLength = anim.length;
                    break;
            }
        }
    }

    public void OnSpawn(float height, FloorController floorController)
    {

        // Prepare to climb
        climbHeight = height;
        state = EnemyStatus.CLIMBING;
        movementBehaviour.speed = climbingSpeed;
        movementBehaviour.direction = Vector2.up;
        currentFloor = floorController;

        // Init life counter
        ResetLife();
    }

    private void Update()
    {
        ManageCurrentState();
        ExecuteNonPhysicsAction();
    }

    private void FixedUpdate()
    {
        ExecutePhysicsAction();
    }

    // Check the correct current state by priority
    private void ManageCurrentState()
    {
        // 1. Actions that can't be interrupted
        if (InBlockingAction()) return;

        // 2. Attack player
        if (ShouldAttackPlayer())
        {
            state = EnemyStatus.ATTACKING;
            return;
        }

        // 3. Chase player
        if (ShouldChasePlayer())
        {
            state = EnemyStatus.CHASING_PLAYER;
            return;
        }

        // 4. Destroy repairable item
        if(ShouldDestroy())
        {
            state = EnemyStatus.DESTROYING;
            return;
        }

        // 5. Move to repairable item
        if(ShouldMoveToCurrentRepairableItem())
        {
            state = EnemyStatus.MOVING_TO_DESTROY;
            return;
        }

        // 6. Change floor
        state = EnemyStatus.MOVING_TO_CHANGE_FLOOR;
    }

    private void ExecuteNonPhysicsAction()
    {
        switch(state)
        {
            case EnemyStatus.ATTACKING:
                if(!attackBehaviour.canAttack) return;

                movementBehaviour.canMove = false;
                attackBehaviour.Attack();
                StartCoroutine(ManageEndAttack());
                
                // Manage animations
                DisableAllAnimationParameters();
                animator.SetBool("attack", true);
                break;

            case EnemyStatus.DESTROYING:
                movementBehaviour.canMove = false;
                if (interactBehaviour.canInteract)
                {
                    interactBehaviour.Interact();
                    interactBehaviour.canInteract = false;
                    Invoke(nameof(EnableInteraction), 1f);

                    // Manage animations
                    DisableAllAnimationParameters();
                    animator.SetBool("interact", true);
                }
                break;

            case EnemyStatus.DYING:
                movementBehaviour.canMove = false;
                animator.SetBool("death", true);
                break;

            case EnemyStatus.CHANGE_FLOOR:
                movementBehaviour.canMove = false;
                interactBehaviour.Interact();
                tpObjectToMove = null;
                break;
        }
    }

    private void ExecutePhysicsAction()
    {
        switch (state)
        {
            case EnemyStatus.CLIMBING:
                if (transform.position.y > climbHeight)
                {
                    // Change state and check for new movement
                    state = EnemyStatus.MOVING_TO_DESTROY;
                    enemyRb.bodyType = RigidbodyType2D.Dynamic;
                    Update();
                    FixedUpdate();
                }
                break;

            case EnemyStatus.CHASING_PLAYER:
                
                // Move left or right depending on the enemy and player positions
                movementBehaviour.canMove = true;
                int toPlayerDirection = transform.position.x > PlayerController.Instance.transform.position.x ? -1 : 1;
                movementBehaviour.direction = new(toPlayerDirection, 0);

                // Manage animations
                DisableAllAnimationParameters();
                animator.SetBool("walk", true);
                break;

            case EnemyStatus.MOVING_TO_DESTROY:
                // Move left or right depending on the enemy and repairable item positions
                movementBehaviour.canMove = true;
                int toRepairableItemDirection = transform.position.x > currentFloor.repairableItem.transform.position.x ? -1 : 1;
                movementBehaviour.direction = new(toRepairableItemDirection, 0);
                
                // Manage animations
                DisableAllAnimationParameters();
                animator.SetBool("walk", true);
                break;

            case EnemyStatus.MOVING_TO_CHANGE_FLOOR:
                // If not set, choose between all the tp objects in the current floor
                if(tpObjectToMove == null)
                {
                    int randomIndex = Random.Range(0, currentFloor.tpObjects.Count);
                    tpObjectToMove = currentFloor.tpObjects[randomIndex];
                }

                // Move left or right depending on the enemy and the tpObject positions
                movementBehaviour.canMove = true;
                int toTpObjectDirection = transform.position.x > tpObjectToMove.transform.position.x ? -1 : 1;
                movementBehaviour.direction = new(toTpObjectDirection, 0);

                // Manage animations
                DisableAllAnimationParameters();
                animator.SetBool("walk", true);
                break;
        }
    }

    private bool InBlockingAction()
    {
        return state == EnemyStatus.CLIMBING
            || state == EnemyStatus.DYING
            || state == EnemyStatus.PARRIED;
    }

    private bool ShouldAttackPlayer()
    {
        // If player is in range and alive
        return playerInAttackRange
            && PlayerController.Instance.currentLife > 0;
    }

    private bool ShouldChasePlayer()
    {
        // If it's not attacking nor climbing, the player is in the same floor and player is alive
        return currentFloor == PlayerController.Instance.currentFloor
            && PlayerController.Instance.currentLife > 0;
    }

    private bool ShouldDestroy()
    {
        // Is in the repairable item collider and is not destroyed
        return interactBehaviour.interactiveObject is RepairableItemBehaviour
            && currentFloor.repairableItem.life > 0;
    }

    private bool ShouldMoveToCurrentRepairableItem()
    {
        // The repairable item of the current floor is not destroyed
        return currentFloor.repairableItem.life > 0;
    }

    private void Climb()
    {
        if (transform.position.y < climbHeight)
        {
            animator.SetBool("climb", true);
            return;
        }
        
        // Change to moving status, apply gravity and constraint rotation
        //status = EnemyStatus.MOVING;
        enemyRb.bodyType = RigidbodyType2D.Dynamic;
        enemyRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        // Change speed and move into the window depending on his facing direction
        movementBehaviour.speed = walkingSpeed;
        movementBehaviour.direction = transform.localScale.x == 1 ? Vector2.right : Vector2.left;
        animator.SetBool("climb", false);
        animator.SetBool("walk", false);
    }

    private void DisableAllAnimationParameters()
    {
        animator.SetBool("climb", false);
        animator.SetBool("walk", false);
        animator.SetBool("attack", false);
        animator.SetBool("death", false);
        animator.SetBool("parried", false);
        animator.SetBool("climb", false);
        animator.SetBool("interact", false);
    }

    // Called in the DetectPlayerBehaviour onEnterTrigger
    public void OnPlayerCollisionEnter() {
        playerInAttackRange = true;
    }

    // Called in the DetectPlayerBehaviour onExitTrigger
    public void OnPlayerCollisionExit()
    {
        playerInAttackRange = false;
    }

    IEnumerator ManageEndAttack()
    {
        yield return new WaitForSeconds(attackAnimLength);

        // Enable movement and end attack
        movementBehaviour.canMove = true;
        attackBehaviour.EndAttack();

        // Change to a new state (CHASING_PLAYER because it's not a blocking action)
        state = EnemyStatus.CHASING_PLAYER;
        ManageCurrentState();
    }

    protected override void ManageHit()
    {
        // Block actions
        movementBehaviour.canMove = false;
        interactBehaviour.canInteract = false;
        attackBehaviour.canAttack = false;

        DisableAllAnimationParameters();
        animator.SetBool("hit", true);
    }

    protected override void ManageDeath()
    {
        state = EnemyStatus.DYING;

        // Block actions
        movementBehaviour.canMove = false;
        interactBehaviour.canInteract = false;
        attackBehaviour.canAttack = false;
        canBeHit = false;

        // Manage animations
        DisableAllAnimationParameters();
        animator.SetBool("death", true);

        Invoke(nameof(DestroyThis), deathAnimLength);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    public void OnParried()
    {
        state = EnemyStatus.PARRIED;

        // End any action and block them
        attackBehaviour.canAttack = false;
        movementBehaviour.canMove = false;
        interactBehaviour.canInteract = false;
        attackBehaviour.canAttack = false;

        // Manage animations
        DisableAllAnimationParameters();
        animator.SetBool("parried", true);

        // Stop other coroutines and start the parried one
        StopAllCoroutines();
        StartCoroutine(ManageEndParried());
    }

    IEnumerator ManageEndParried()
    {
        yield return new WaitForSeconds(parriedAnimLength);

        // Enable actions
        movementBehaviour.canMove = true;
        interactBehaviour.canInteract = true;
        attackBehaviour.canAttack = true;

        // Manage animations
        animator.SetBool("parried", false);

        // Change to a new state (CHASING_PLAYER because it's not a blocking action)
        state = EnemyStatus.CHASING_PLAYER;
        ManageCurrentState();
    }

    public void EnableInteraction()
    {
        interactBehaviour.canInteract = true;
    }
}
