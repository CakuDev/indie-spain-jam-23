using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;

    public Vector2 Direction { get; set; } = Vector2.zero;
    public float Modifier { get; set; } = 1f;

    void FixedUpdate()
    {
        transform.Translate(speed * Modifier * Direction);
    }
}
