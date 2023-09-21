using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;

    public Vector2 Direction { get; set; } = Vector2.zero;
    public float Modifier { get; set; } = 1f;
    public bool CanMove { get; set; } = true;

    void FixedUpdate()
    {
        // Avoid movement logic
        if (Direction == Vector2.zero || !CanMove)
        {
            animator.SetBool("walk", false);
            return;
        }

        animator.SetBool("walk", true);
        transform.Translate(speed * Modifier * Time.deltaTime * Direction);   
        ManageFacingDirection();
    }

    private void ManageFacingDirection()
    {
        // Change the facing direction depending on the movement
        if (Direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
