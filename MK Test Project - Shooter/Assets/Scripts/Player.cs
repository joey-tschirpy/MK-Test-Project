using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] Joystick moveStick;
    [SerializeField] Joystick shootStick;

    [SerializeField] float moveSpeed;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float deltaTime = Time.deltaTime;

        // Shoot will override look direction from move
        Move(deltaTime);
        Shoot(deltaTime);
	}

    private void LookDirection(Joystick joystick)
    {
        if (joystick.Direction.magnitude <= 0f) return;

        Vector3 lookDirection = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        rb.rotation = Quaternion.LookRotation(lookDirection);
    }

    private void Move(float deltaTime)
    {
        float speed = moveSpeed * deltaTime;
        rb.velocity = new Vector3(moveStick.Horizontal * moveSpeed, rb.velocity.y, moveStick.Vertical * moveSpeed);

        // Look in direction of movement
        LookDirection(moveStick);
    }

    private void Shoot(float deltaTime)
    {
        // Override look direction of movement with shoot direction
        LookDirection(shootStick);

    }
}
