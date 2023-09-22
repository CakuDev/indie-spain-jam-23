using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private MovementBehaviour movementBehaviour;
    [SerializeField] private AttackBehaviour attackBehaviour;
    [SerializeField] private InteractBehaviour interactBehaviour;
    [SerializeField] private Rigidbody2D enemyRb;
    [SerializeField] private float climbingSpeed = 2f;
    [SerializeField] private float walkingSpeed = 4f;

    private float climbHeight;
    private EnemyStatus status;

    public void Start()
    {
        movementBehaviour.direction = Vector2.up;
        OnSpawn(-0.7f);
    }

    private void Update()
    {
        switch (status)
        {
            case EnemyStatus.ATTACKING:
                attackBehaviour.Attack();
                break;
        }
    }

    private void FixedUpdate()
    {
        switch(status)
        {
            case EnemyStatus.CLIMBING:
                Climb();
                break;
        }
    }

    public void OnSpawn(float height)
    {
        climbHeight = height;
        status = EnemyStatus.CLIMBING;
        movementBehaviour.speed = climbingSpeed;
    }

    private void Climb()
    {
        if (transform.position.y < climbHeight) return;
        
        // Change to moving status, apply gravity and constraint rotation
        status = EnemyStatus.MOVING;
        enemyRb.bodyType = RigidbodyType2D.Dynamic;
        enemyRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        // Change speed and move into the window depending on his facing direction
        movementBehaviour.speed = walkingSpeed;
        movementBehaviour.direction = transform.localScale.x == 1 ? Vector2.right : Vector2.left;
    }

    // Called in the DetectPlayerBehaviour onEnterTrigger
    public void OnPlayerCollisionEnter() {
        movementBehaviour.canMove = false;
        status = EnemyStatus.ATTACKING;
    }

    // Called in the DetectPlayerBehaviour onExitTrigger
    public void OnPlayerCollisionExit()
    {
        status = EnemyStatus.MOVING;
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
}
