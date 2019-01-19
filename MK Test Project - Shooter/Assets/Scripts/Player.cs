using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] Joystick moveStick;
    [SerializeField] Joystick shootStick;

    [Tooltip ("1 equates to normal speed")]
    [Range(0,2)] [SerializeField] float moveSpeed = 1;

    private Rigidbody rb;
    private Animator anim;

    private Vector2 moveDirection;
    private Vector2 shootDirection;

    // set true to use controller
    [SerializeField] private const bool TESTING = true;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.speed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        float deltaTime = Time.deltaTime;
        UpdateDirections();

        // Shoot will override look direction from move
        Move(deltaTime);
        Shoot(deltaTime);
        SetMoveAnimation();
	}

    private void UpdateDirections()
    {
        if (TESTING)
        {
            moveDirection = new Vector2(Input.GetAxis("Horizontal Left"), Input.GetAxis("Vertical Left"));
            shootDirection = new Vector2(Input.GetAxis("Horizontal Right"), Input.GetAxis("Vertical Right"));
        }
        else
        {
            moveDirection = moveStick.Direction;
            shootDirection = shootStick.Direction;
        }
    }

    private void SetMoveAnimation()
    {
        
        
        // Forward move animation if angle between move direction and shoot direction
        // is less than or equal to 90 degrees, else backward move animation.
        float move = Vector2.Dot(moveDirection, shootDirection) >= 0f ?
            moveDirection.magnitude : -moveDirection.magnitude;
        anim.SetFloat("Movement", move);
    }

    private void LookDirection(Vector2 direction)
    {
        if (direction.magnitude <= 0f) return;

        Vector3 lookDirection = new Vector3(direction.x, 0f, direction.y);
        rb.rotation = Quaternion.LookRotation(lookDirection);
    }

    private void Move(float deltaTime)
    {
        float speed = moveSpeed * deltaTime * 800f;

        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.y * speed);

        // Look in direction of movement
        LookDirection(moveDirection);
    }

    private void Shoot(float deltaTime)
    {
        // Override look direction of movement with shoot direction
        LookDirection(shootDirection);

    }
}
