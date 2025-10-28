using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    public float speed;
    [SerializeField] private Animator animator;

    [HideInInspector] public Vector2 direction = Vector2.zero;
    [HideInInspector] public float modifier = 1f;
     public bool canMove = true;

    void FixedUpdate()
    {
        // Avoid movement logic
        if (direction == Vector2.zero || !canMove)
        {
            animator.SetBool("walk", false);
            return;
        }

        animator.SetBool("walk", true);
        transform.Translate(speed * modifier * Time.deltaTime * direction);   
        ManageFacingDirection();
    }

    private void ManageFacingDirection()
    {
        // Change the facing direction depending on the movement
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
