using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Weapon Data")]
public class WeaponData : ScriptableObject {

    [Tooltip("Shots per second")] [Range (0.1f, 100f)] [SerializeField] float fireRate;
    [SerializeField] GameObject muzzleFlashPrefab;
    [SerializeField] string projectileTag;
    [SerializeField] GameObject shootSoundPrefab;

    public float FireRate()
    {
        return fireRate;
    }

    public GameObject MuzzleFlashPrefab()
    {
        return muzzleFlashPrefab;
    }

    public string ProjectileTag()
    {
        return projectileTag;
    }

    public GameObject ShootSoundPrefab()
    {
        return shootSoundPrefab;
    }
}
