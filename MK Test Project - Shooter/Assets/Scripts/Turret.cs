using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    [SerializeField] WeaponData weaponData;
    [SerializeField] GameObject projectileSpawnPoint;
    private float projectileSpawnTimer;
    private bool isShooting;

    // Use this for initialization
    void Start () {
        projectileSpawnTimer = 0f;
        isShooting = true;
    }
	
	// Update is called once per frame
	void Update () {
        float deltaTime = Time.deltaTime;

        Shoot(deltaTime);
	}

    private void Shoot(float deltaTime)
    {
        if (isShooting)
        {
            projectileSpawnTimer += deltaTime;

            if (projectileSpawnTimer >= 1f / weaponData.FireRate())
            {
                // Spawn projectile
                Instantiate(weaponData.ProjectilePrefab(),
                    projectileSpawnPoint.transform.position, transform.rotation);
                projectileSpawnTimer = 0f;

                // Spawn muzzle flash particle
                GameObject muzzleFlash = Instantiate(weaponData.MuzzleFlashPrefab(),
                    projectileSpawnPoint.transform.position, transform.rotation);
                muzzleFlash.transform.SetParent(projectileSpawnPoint.transform);

            }
        }
        else
        {
            projectileSpawnTimer = 0f;
        }
    }
}
