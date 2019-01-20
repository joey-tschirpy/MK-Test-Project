using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Projectile Data")]
public class ProjectileData : ScriptableObject {
    [SerializeField] float damage;
    [SerializeField] float speed;

    public float Damage()
    {
        return damage;
    }

    public float Speed()
    {
        return speed;
    }
}
