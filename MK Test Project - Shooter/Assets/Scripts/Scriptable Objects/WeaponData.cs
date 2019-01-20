using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Weapon Data")]
public class WeaponData : ScriptableObject {

    [Tooltip("Shots per second")] [Range (0.1f, 100f)] [SerializeField] float fireRate;
    [SerializeField] GameObject muzzleFlashPrefab;
    [SerializeField] GameObject projectilePrefab;

    public float FireRate()
    {
        return fireRate;
    }

    public GameObject MuzzleFlashPrefab()
    {
        return muzzleFlashPrefab;
    }

    public GameObject ProjectilePrefab()
    {
        return projectilePrefab;
    }
}
