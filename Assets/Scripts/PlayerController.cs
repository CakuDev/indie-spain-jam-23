using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader input;

    [SerializeField] private float speed;

    [SerializeField] private Vector2 _moveDirection;

    private void Start()
    {
        input.MoveEvent += HandleMove;

        input.InteractEvent += HandleInteract;
    }

    private void HandleInteract()
    {
        Debug.Log("I interact");
    }

    private void Update()
    {
       Move();
    }

    private void HandleMove(Vector2 dir)
    {
        _moveDirection = dir;
    }

    private void Move()
    {
        if (_moveDirection == Vector2.zero)
        {
            return;
        }
        transform.position += new Vector3(_moveDirection.x,0,0) * (speed*Time.deltaTime);
    }

}
