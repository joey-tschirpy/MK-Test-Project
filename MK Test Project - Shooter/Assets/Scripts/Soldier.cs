using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour {
    // Enemy variables
    [SerializeField] MovementData moveData;
    private Animator anim;

    // Shooting variables
    [SerializeField] WeaponData weapon;
    [SerializeField] GameObject projectileSpawnPoint;
    private float projectileSpawnTimer;

    // AI variables
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] GameObject objective;
    [SerializeField] float shootDistanceThreshold;
    [SerializeField] float playerDistanceThreshold;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        anim.speed = moveData.MoveSpeed();

        projectileSpawnTimer = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        float deltaTime = Time.deltaTime;

        // Shoot will override look direction from move
        Positioning(deltaTime);
        SetMoveAnimation();
    }

    private void Positioning(float deltaTime)
    {
        Vector3 toPlayer = objective.transform.position - transform.position;
        navMeshAgent.isStopped = false;

        float distToPlayer = toPlayer.magnitude;
        if (distToPlayer > shootDistanceThreshold)
        {
            navMeshAgent.SetDestination(objective.transform.position);
        }
        else if (distToPlayer < playerDistanceThreshold)
        {
            navMeshAgent.SetDestination(-toPlayer.normalized * playerDistanceThreshold);
            Shoot(deltaTime, toPlayer);
        }
        else
        {
            navMeshAgent.isStopped = true;
            Shoot(deltaTime, toPlayer);
        }
    }

    private void SetMoveAnimation()
    {
        // Forward move animation if angle between move direction and shoot direction
        // is less than or equal to 90 degrees, else backward move animation.
        float move = Vector2.Dot(navMeshAgent.velocity, navMeshAgent.transform.forward) >= 0f ?
            navMeshAgent.velocity.magnitude : -navMeshAgent.velocity.magnitude;
        anim.SetFloat("Movement", move);
    }

    public void SetObjective(GameObject objective)
    {
        this.objective = objective;
    }

    private void Shoot(float deltaTime, Vector3 shootDirection)
    {
        navMeshAgent.transform.rotation = Quaternion.LookRotation(shootDirection, Vector3.up);

        projectileSpawnTimer += deltaTime;

        // Only shoot if aiming at player (ignore projectile layer, 9)
        int layerMask = ~(1 << 9);
        RaycastHit hit;
        if (Physics.Raycast(projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.name == "Player" && projectileSpawnTimer >= 1f / weapon.FireRate())
            {
                // Spawn projectile
                ProjectilePooler.Instance.SpawnProjectile(weapon.ProjectileTag(),
                    projectileSpawnPoint.transform.position, navMeshAgent.transform.rotation);
                projectileSpawnTimer = 0f;

                // Spawn muzzle flash particle
                GameObject muzzleFlash = Instantiate(weapon.MuzzleFlashPrefab(),
                    projectileSpawnPoint.transform.position, navMeshAgent.transform.rotation);
                muzzleFlash.transform.SetParent(projectileSpawnPoint.transform);
            }
        }
    }
}
