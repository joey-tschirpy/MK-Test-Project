using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] Joystick moveStick;
    [SerializeField] Joystick shootStick;

    // Player variables
    [SerializeField] PlayerData playerData;
    private Rigidbody rb;
    private Animator anim;
    private Vector2 moveDirection;
    private Vector2 shootDirection;

    // Player shooting variables
    [SerializeField] WeaponData weaponData;
    [SerializeField] GameObject projectileSpawnPoint;
    private float projectileSpawnTimer;


    // set true to use controller, false for virtual joysticks
    [SerializeField] private const bool TESTING = false;

    // Use this for initialization
    void Start () {
        playerData.init();

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.speed = playerData.MoveSpeed();

        projectileSpawnTimer = 0f;
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

    public void ChangeWeapon(WeaponData weapon)
    {
        weaponData = weapon;
    }

    private void Move(float deltaTime)
    {
        float speed = playerData.MoveSpeed() * deltaTime * 800f;

        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.y * speed);

        // Look in direction of movement
        SetLookDirection(moveDirection);
    }

    private bool SetLookDirection(Vector2 direction)
    {
        if (direction.magnitude <= 0f) return false;

        Vector3 lookDirection = new Vector3(direction.x, 0f, direction.y);
        rb.rotation = Quaternion.LookRotation(lookDirection);

        return true;
    }

    private void SetMoveAnimation()
    {
        // Forward move animation if angle between move direction and shoot direction
        // is less than or equal to 90 degrees, else backward move animation.
        float move = Vector2.Dot(moveDirection, shootDirection) >= 0f ?
            moveDirection.magnitude : -moveDirection.magnitude;
        anim.SetFloat("Movement", move);
    }

    private void Shoot(float deltaTime)
    {
        // Override look direction of movement with shoot direction
        bool isShooting = SetLookDirection(shootDirection);

        if (isShooting)
        {
            projectileSpawnTimer += deltaTime;

            if (projectileSpawnTimer >= 1f / weaponData.FireRate())
            {
                // Spawn projectile

                ProjectilePooler.Instance.SpawnProjectile(weaponData.ProjectileTag(),
                    projectileSpawnPoint.transform.position, rb.rotation);
                projectileSpawnTimer = 0f;

                // Spawn muzzle flash particle
                GameObject muzzleFlash = Instantiate(weaponData.MuzzleFlashPrefab(),
                    projectileSpawnPoint.transform.position, rb.rotation);
                muzzleFlash.transform.SetParent(projectileSpawnPoint.transform);

            }
        }
        else
        {
            projectileSpawnTimer = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        playerData.TakeDamage(damage);
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
}
