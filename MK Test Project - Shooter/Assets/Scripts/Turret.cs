using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Turret : MonoBehaviour {
    [SerializeField] WeaponData weapon;
    [SerializeField] GameObject projectileSpawnPoint;
    private float projectileSpawnTimer;

    // AI variables
    [SerializeField] GameObject turretHead;
    [Tooltip ("Degrees per second")] [SerializeField] float rotateSpeed;
    [SerializeField] float angleThreshold = 5f;
    [SerializeField] float distanceThreshold = 40f;
    [SerializeField] GameObject objective;
    Vector3 toPlayer;
    float rightAngleToPlayer;
    float angleToPlayer;

    // Use this for initialization
    void Start () {
        projectileSpawnTimer = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        float deltaTime = Time.deltaTime;
        toPlayer = objective.transform.position - transform.position;
        rightAngleToPlayer = Vector3.Angle(projectileSpawnPoint.transform.right, toPlayer.normalized);
        angleToPlayer = Vector3.Angle(projectileSpawnPoint.transform.forward, toPlayer.normalized);

        Aiming(deltaTime);
        Shoot(deltaTime);
	}

    private void Aiming(float deltaTime)
    {
        if (rightAngleToPlayer < 89.5f)
        {
            // Rotate right
            turretHead.transform.Rotate(Vector3.up, rotateSpeed * deltaTime);
        }
        else if (rightAngleToPlayer > 90.5f)
        {
            // Rotate left
            turretHead.transform.Rotate(Vector3.up, -rotateSpeed * deltaTime);
        }
        else if (angleToPlayer > 179f)
        {
            // Facing backward
            // Rotate right
            turretHead.transform.Rotate(Vector3.up, rotateSpeed * deltaTime);
        }
    }

    public void SetObjective(GameObject objective)
    {
        this.objective = objective;
    }

    private void Shoot(float deltaTime)
    {
        if (angleToPlayer > angleThreshold || toPlayer.magnitude > distanceThreshold)
        {
            return;
        }
        
        // Start shooting at player if in view (ignoring projectile layer, 9)
        int layerMask = ~(1 << 9);
        RaycastHit hit;
        if (Physics.Raycast(projectileSpawnPoint.transform.position, toPlayer, out hit, Mathf.Infinity, layerMask))
        {
            projectileSpawnTimer += deltaTime;
            if (hit.transform.name == "Player" && projectileSpawnTimer >= 1f / weapon.FireRate())
            {
                // Spawn projectile
                ProjectilePooler.Instance.SpawnProjectile(weapon.ProjectileTag(),
                    projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
                projectileSpawnTimer = 0f;

                // Spawn muzzle flash particle
                GameObject muzzleFlash = Instantiate(weapon.MuzzleFlashPrefab(),
                    projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
                muzzleFlash.transform.SetParent(projectileSpawnPoint.transform);
            }
        }
    }
}
