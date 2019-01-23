using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Collectible Weapon Data")]
public class CollectibleWeaponData : ScriptableObject {
    [SerializeField] float respawnTime;
    [SerializeField] float timer;
    [SerializeField] WeaponData weapon;

    public float RespawnTime()
    {
        return respawnTime;
    }

    public float Timer()
    {
        return timer;
    }

    public WeaponData Weapon()
    {
        return weapon;
    }
}
